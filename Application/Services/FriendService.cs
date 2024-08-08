using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _repository;
        private readonly IHttpContextAccessor _http;

        public FriendService(IFriendRepository repository, IHttpContextAccessor http)
        {
            _repository = repository;
            _http = http;
        }

        public async Task<ApiResult> AcceptFriendRequest(Guid userId2, bool status)
        {
            await _repository.AcceptFriendRequest(userId2, status);
            return new ApiResult { Msg = "درخواست دوستی بروز شد", Success = true };
        }

        public async Task<ApiResult> AddFriendRequest(Guid userId2)
        {
            var userId = (Guid)_http.HttpContext.Items["userId"];

            var ct = await _repository.ReqCount(userId, userId2);
            if (ct > 0)
                return new ApiResult { Msg = "شما قبلا برای این شخص درخواست فرستاده اید" };

            await _repository.AddFriendRequest(new Friend
            {
                Confrimed = false,
                DateTime = DateTime.UtcNow,
                UserId1 = userId,
                UserId2 = userId2
            });
            return new ApiResult { Msg = "درخواست دوستی ارسال شد", Success = true };
        }

        public async Task<ApiResult<List<Friend>>> GetFriends()
        {
            var userId = (Guid)_http.HttpContext.Items["userId"];
            var res = await _repository.GetFriends(userId);
            return new ApiResult<List<Friend>> { Data = res.ToList(), Success = true };
        }
    }
}
