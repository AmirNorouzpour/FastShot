using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IMsgService
    {
        Task<List<Msg>> GetUserMsg(int page);
        Task<ApiResult> AddMsg(string title, string body, string icon, Guid userId, MsgType type);
    }
}
