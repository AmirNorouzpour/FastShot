using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ApiResult<AuthenticateResponse>> Authenticate(AuthenticateReq model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return new ApiResult<AuthenticateResponse> { Success = false, Msg = "نام کاربری و یا رمز عبور اشتباه است" };

            return new ApiResult<AuthenticateResponse> { Success = true, Data = response };
        }

        [HttpPost("register")]
        public async Task<ApiResult> Register(RegisterUserModel model)
        {
            var response = await _userService.RegisterUser(model);
            return response;
        }

        [HttpPost("verify")]
        public async Task<ApiResult<AuthenticateResponse>> Verify(SsoVerifyModel model)
        {
            var response = await _userService.VerifyUser(model);
            return response;
        }

        [HttpGet("getUserInfo")]
        public async Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId)
        {
            var res = await _userService.GetUserInfo(userId);
            return res;
        }

        [HttpGet("getLeadersBoard")]
        public async Task<ApiResult<LeadersBoardResult>> GetLeadersBoard(Guid userId)
        {
            var res = await _userService.GetLeadersBoard(userId);
            return new ApiResult<LeadersBoardResult> { Success = true, Data = res };
        }

        [HttpPost("updateUsername")]
        public async Task<ApiResult<string>> UpdateUsername(UserDataDto model)
        {
            var response = await _userService.UpdateUsername(model.UserName);
            return response;
        }

        [HttpPost("updateSheba")]
        public async Task<ApiResult<string>> UpdateSheba(UserDataDto model)
        {
            var response = await _userService.UpdateSheba(model.Sheba);
            return response;
        }

    }
}
