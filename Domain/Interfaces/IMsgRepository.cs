using Domain.Models;

namespace Domain.Interfaces
{
    public interface IMsgRepository
    {
        Task AddMsg(Msg msg);
        Task<IEnumerable<Msg>> GetUserMsgs(Guid userId, int page);
    }
}
