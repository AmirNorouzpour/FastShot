﻿using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly IOtpService _otpService;
        private readonly IHttpContextAccessor _http;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, ISecurityService securityService, IOtpService otpService, IHttpContextAccessor http)
        {
            _userRepository = userRepository;
            _securityService = securityService;
            _otpService = otpService;
            _http = http;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateReq model)
        {
            var hashedPassword = _securityService.GetMd5(model?.Password);
            var user = await _userRepository.Authenticate(model?.Username, hashedPassword);

            if (user == null) return null;

            var token = await generateJwtToken(user);

            return new AuthenticateResponse { ExpireDate = DateTime.Now.AddDays(70), Token = token, UserId = user.Id };
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

                var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(70),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
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

        public async Task<ApiResult<AuthenticateResponse>> VerifyUser(SsoVerifyModel model)
        {
            var isdebug = Debugger.IsAttached;
            var otpValidateResult = false;
            if (!isdebug)
                otpValidateResult = await _otpService.CheckCode(model.Receptor, model.Code, model.SsoType);

            if (otpValidateResult || isdebug)
            {
                if (model.SsoType == 0)
                {
                    var user = await _userRepository.GetUserByMobile(model.Receptor);
                    if (user == null)
                        return new ApiResult<AuthenticateResponse> { Msg = "کاربری یافت نشد" };
                    //todo : remove other user sessions

                    user.MobileVerified = true;
                    user.IsActive = true;
                    user.SsoType = model.SsoType;
                    user.LastUpdateDateTime = DateTime.UtcNow;
                    await _userRepository.UpdateUser(user);
                    var token = await generateJwtToken(user);
                    return new ApiResult<AuthenticateResponse>
                    {
                        Msg = "کاربر با موفقیت فعال شد",
                        Success = true,
                        Data = new AuthenticateResponse
                        {
                            ExpireDate = DateTime.UtcNow.AddDays(70),
                            Token = token,
                            UserId = user.Id
                        }
                    };
                }
            }
            return new ApiResult<AuthenticateResponse> { Msg = "کد وارد شده صحیح نمی باشد" };
        }

        public async Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
                return new ApiResult<UserInfoModel> { Msg = "کاربری یافت نشد" };

            var lastNoteId = await GetUserLastNoteId(userId);
            var userActiveRoomRuns = await GetUserActiveRoomRuns(userId);
            var wins = await _userRepository.GetUserWinsAndPlays(userId);

            var res = new UserInfoModel
            {
                Balance = user.Credit,
                UserId = userId,
                UserName = user.UserName,
                LastNoteId = lastNoteId,
                ActiveRoomRuns = userActiveRoomRuns.ToList(),
                WinsCount = wins[1],
                PlaysCount = wins[0],
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

        public async Task<LeadersBoardResult> GetLeadersBoard(Guid userId)
        {
            var res = await _userRepository.GetLeadersBoard(userId);
            return res;
        }

        public async Task<UserExtraFieldsModel> GetUserBalance(Guid userId, long roomRunId)
        {
            var res = await _userRepository.GetUserBalance(userId, roomRunId);
            return res;
        }

        public async Task<ApiResult<string>> UpdateUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new ApiResult<string> { Msg = "مقادیر ورودی نباید خالی باشد" };
            }

            var userId = (Guid)_http.HttpContext.Items["userId"];
            var user = await _userRepository.GetById(userId);

            if (user != null && user.Id != userId)
                return new ApiResult<string> { Msg = "شناسه متعلق به شما نیست" };

            if (user == null)
                return new ApiResult<string> { Msg = "کاربری وجود ندارد" };

            await _userRepository.UpdateUsername(username, userId);
            return new ApiResult<string> { Success = true };
        }

        public async Task<ApiResult<string>> UpdateSheba(string? sheba)
        {
            if (string.IsNullOrWhiteSpace(sheba))
            {
                return new ApiResult<string> { Msg = "مقادیر ورودی نباید خالی باشد" };
            }
            var userId = (Guid)_http.HttpContext.Items["userId"];
            await _userRepository.UpdateSheba(sheba, userId);
            return new ApiResult<string> { Success = true };
        }

        public async Task<IEnumerable<User>> GetAll(Dictionary<string, object> parameters)
        {
            return await _userRepository.GetAll(parameters);
        }

        public async Task<int> Count(Dictionary<string, object> parameters)
        {
            return await _userRepository.Count(parameters);
        }

        public async Task<ApiResult<LangModel>> GetLang(string ip, Guid? userId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.findip.net/{ip}/?token={"dad02add75244db682479415389f2bde"}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonString);

                    if (userId.HasValue)
                    {
                        var user = await _userRepository.GetById(userId.Value);
                        if (user == null)
                            return new ApiResult<LangModel> { Msg = "کاربری یافت نشد" };

                        user.Ip = ip;
                        user.Country = data["country"]["names"]["en"].Value<string>();
                        user.City = data["city"]["names"]["en"].Value<string>();
                        user.Isp = data["traits"]["organization"].Value<string>();
                        await _userRepository.UpdateUser(user);
                    }

                    if (data["country"]["names"]["en"].Value<string>() == "Iran")
                        return new ApiResult<LangModel> { Data = new LangModel { country = data["country"]["names"]["en"].Value<string>(), Lang = "fa-IR", ip = ip } };

                    return new ApiResult<LangModel> { Data = new LangModel { country = data["country"]["names"]["en"].Value<string>(), Lang = "en-US", ip = ip } };
                }
                return null;

            }
        }
    }
}
