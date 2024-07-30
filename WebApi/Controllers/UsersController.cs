using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ApiResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return new ApiResult<AuthenticateResponse> { Success = false, Msg = "نام کاربری و یا رمز عبور اشتباه است" };

            return new ApiResult<AuthenticateResponse> { Success = true, Data = response };
        }

        [HttpPost("register")]
        public async Task<ApiResult> Register(AddRawUser model)
        {
            var response = await _userService.AddRawUser(model);
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] User userObj)
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }
    }
}
