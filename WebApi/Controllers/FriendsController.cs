using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<Friend>>> Get()
        {
            var response = await _friendService.GetFriends();
            return response;
        }

        [HttpPost]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult> AddFriendRequest(Friend friend)
        {
            var response = await _friendService.AddFriendRequest(friend.UserId2);
            return response;
        }

        [HttpPost]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult> AcceptFriendRequest(Friend friend)
        {
            var response = await _friendService.AcceptFriendRequest(friend.UserId2, friend.Confrimed.GetValueOrDefault());
            return response;
        }
    }
}
