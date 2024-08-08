using Domain.Models;

namespace Domain.Interfaces
{
    public interface IFriendRepository
    {
        Task AddFriendRequest(Friend friend);
        Task AcceptFriendRequest(Guid userId2, bool status);
        Task<IEnumerable<Friend>> GetFriends(Guid userId);
        Task<int> ReqCount(Guid userId, Guid userId2);
    }
}
