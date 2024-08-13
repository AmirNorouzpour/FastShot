using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<User> RegisterUser(User model);
        Task<User?> Authenticate(string? username, string? password);
        Task<User?> GetUserByMobile(string? mobile);
        Task UpdateUser(User user);
        Task<long> GetUserLastNoteId(Guid userId);
        Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId);
        Task<List<int>> GetUserWinsAndPlays(Guid userId);
        Task<LeadersBoardResult> GetLeadersBoard(Guid userId);
        Task<UserExtraFieldsModel> GetUserBalance(Guid userId, long roomRunId);
        Task UpdateUsername(string username, Guid userId);
        Task UpdateSheba(string sheba, Guid userId);
        Task<User?> GetUserByUserName(string username);
        Task<IEnumerable<User>> GetAll(Dictionary<string, object> parameters);
        Task<int> Count(Dictionary<string, object> parameters);
    }
}
