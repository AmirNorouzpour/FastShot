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
        private IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
            _appSettings = appSettings.Value;
        }

        public Task<User?> AddAndUpdateUser(User userObj)
        {
            return _userRepository.AddAndUpdateUser(userObj);
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
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

        public Task<User?> GetById(int id)
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
    }
}
