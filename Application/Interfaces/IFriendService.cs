using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IFriendService
    {
        Task<ApiResult<List<Friend>>> GetFriends();
        Task<ApiResult> AddFriendRequest(Guid userId2);
        Task<ApiResult> AcceptFriendRequest(Guid userId2, bool status);
    }
}
