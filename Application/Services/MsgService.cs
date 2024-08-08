using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class MsgService : IMsgService
    {
        private readonly IMsgRepository _repository;
        private readonly IHttpContextAccessor _http;

        public MsgService(IMsgRepository repository, IHttpContextAccessor http)
        {
            _repository = repository;
            _http = http;
        }

        public async Task<ApiResult> AddMsg(string title, string body, string icon, Guid userId, MsgType type)
        {
            await _repository.AddMsg(new Msg { Title = title, Body = body, Icon = icon, UserId = userId, DateTime = DateTime.UtcNow, MsgType = type });
            return new ApiResult { Success = true };
        }

        public async Task<List<Msg>> GetUserMsg(int page)
        {
            var userId = (Guid)_http.HttpContext.Items["userId"];
            var res = await _repository.GetUserMsgs(userId, page);
            return res.ToList();
        }
    }
}
