using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMsgService _msgService;

        public UsersController(IUserService userService, IMsgService msgService)
        {
            _userService = userService;
            _msgService = msgService;
        }

        [HttpPost("authenticate")]
        [Authorize]
        public async Task<ApiResult<AuthenticateResponse>> Authenticate(AuthenticateReq model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return new ApiResult<AuthenticateResponse> { Success = false, Msg = "نام کاربری و یا رمز عبور اشتباه است" };

            return new ApiResult<AuthenticateResponse> { Success = true, Data = response };
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<ApiResult> Register(RegisterUserModel model)
        {
            var response = await _userService.RegisterUser(model);
            return response;
        }

        [HttpPost("verify")]
        [Authorize]
        public async Task<ApiResult<AuthenticateResponse>> Verify(SsoVerifyModel model)
        {
            var response = await _userService.VerifyUser(model);
            return response;
        }

        [HttpGet("getUserInfo")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId)
        {
            var res = await _userService.GetUserInfo(userId);
            return res;
        }

        [HttpGet("getLeadersBoard")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<LeadersBoardResult>> GetLeadersBoard(Guid userId)
        {
            var res = await _userService.GetLeadersBoard(userId);
            return new ApiResult<LeadersBoardResult> { Success = true, Data = res };
        }

        [HttpPost("updateUsername")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<string>> UpdateUsername(UserDataDto model)
        {
            var response = await _userService.UpdateUsername(model.UserName);
            return response;
        }

        [HttpPost("updateSheba")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<string>> UpdateSheba(UserDataDto model)
        {
            var response = await _userService.UpdateSheba(model.Sheba);
            return response;
        }

        [HttpGet("getuserMsgs")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<Msg>>> GetUserMsgs(int page)
        {
            var res = await _msgService.GetUserMsg(page);
            return new ApiResult<List<Msg>> { Success = true, Data = res };
        }

    }
}
