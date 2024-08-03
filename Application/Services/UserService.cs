using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly IOtpService _otpService;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, ISecurityService securityService, IOtpService otpService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
            _otpService = otpService;
            _appSettings = appSettings.Value;
        }

        public Task<User?> AddAndUpdateUser(User userObj)
        {
            return _userRepository.AddAndUpdateUser(userObj);
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateReq model)
        {
            var hashedPassword = _securityService.GetMd5(model?.Password);
            var user = await _userRepository.Authenticate(model?.Username, hashedPassword);

            if (user == null) return null;

            var token = await generateJwtToken(user);

            return new AuthenticateResponse { ExpireDate = DateTime.Now.AddDays(70), Token = token, UserId = user.Id };
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<User?> GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        private async Task<string> generateJwtToken(User user)
        {
            //Generate token that is valid for 70 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(70),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }

        public async Task<ApiResult> RegisterUser(RegisterUserModel model)
        {
            var id = Guid.NewGuid();

            if (model.SsoType == 0 && string.IsNullOrWhiteSpace(model.Mobile))
                return new ApiResult { Msg = "شماره وارد شده صحیح نمی باشد" };

            else if (model.SsoType == 0 && !string.IsNullOrWhiteSpace(model.Mobile))
            {
                var count = await _otpService.GetCount(model.Mobile);
                if (count >= 3)
                    return new ApiResult { Msg = "در هر 30 دقیقه فقط 3 بار می توانید درخواست کد بدهید" };

                var sendRes = await _otpService.SendSms(model.Mobile);

                if (!sendRes)
                    return new ApiResult { Msg = "مشکلی در ارسال پیامک وجود دارد لطفا کمی بعد تلاش کنید" };

                var dbUser = await _userRepository.GetUserByMobile(model.Mobile);
                if (dbUser != null)
                {
                    return new ApiResult { Msg = "کد اعتبار سنجی با موفقیت ارسال شد", Data = dbUser.Id, Success = true };
                }
                else
                {
                    await _userRepository.RegisterUser(new User
                    {
                        DeviceName = model.DeviceName,
                        DeviceUid = model.DeviceUid,
                        Mobile = model.Mobile,
                        IsActive = false,
                        RegisterDateTime = DateTime.UtcNow,
                        SsoType = model.SsoType,
                        Id = id
                    });
                    return new ApiResult { Msg = "کد اعتبار سنجی با موفقیت ارسال شد", Data = dbUser.Id, Success = true };
                }
            }


            if (model.SsoType == 1)
            {
                //_otpService.SendEmail(model.Email);
                return new ApiResult { Msg = "نوع ورود پیاده سازی نشده است" };
            }

            if (model.SsoType == 2 && !string.IsNullOrWhiteSpace(model.Email))
            {
                await _userRepository.RegisterUser(new User
                {
                    DeviceName = model.DeviceName,
                    DeviceUid = model.DeviceUid,
                    Email = model.Email,
                    IsActive = true,
                    RegisterDateTime = DateTime.UtcNow,
                    SsoType = model.SsoType,
                    Id = id
                });
            }

            return new ApiResult { Msg = "نوع ورود نامشخص است" };
        }

        public async Task<ApiResult<Guid>> VerifyUser(SsoVerifyModel model)
        {
            var otpValidateResult = await _otpService.CheckCode(model.Receptor, model.Code, model.SsoType);
            if (otpValidateResult)
            {
                if (model.SsoType == 0)
                {
                    var user = await _userRepository.GetUserByMobile(model.Receptor);
                    if (user == null)
                        return new ApiResult<Guid> { Msg = "کاربری یافت نشد" };

                    user.MobileVerified = true;
                    user.IsActive = true;
                    user.SsoType = model.SsoType;
                    user.LastUpdateDateTime = DateTime.UtcNow;
                    await _userRepository.UpdateUser(user);
                    return new ApiResult<Guid> { Msg = "کاربر با موفقیت فعال شد", Success = true, Data = user.Id };
                }
            }
            return new ApiResult<Guid> { Msg = "کد وارد شده صحیح نمی باشد" };
        }

        public async Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
                return new ApiResult<UserInfoModel> { Msg = "کاربری یافت نشد" };

            var lastNoteId = await GetUserLastNoteId(userId);
            var userActiveRoomRuns = await GetUserActiveRoomRuns(userId);

            var res = new UserInfoModel
            {
                Balance = user.Credit,
                UserId = userId,
                UserName = user.UserName,
                LastNoteId = lastNoteId,
                ActiveRoomRuns = userActiveRoomRuns.ToList(),
            };
            return new ApiResult<UserInfoModel> { Success = true, Data = res };
        }

        public async Task<long> GetUserLastNoteId(Guid userId)
        {
            var res = await _userRepository.GetUserLastNoteId(userId);
            return res;
        }

        public async Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId)
        {
            var res = await _userRepository.GetUserActiveRoomRuns(userId);
            return res;
        }
    }
}
