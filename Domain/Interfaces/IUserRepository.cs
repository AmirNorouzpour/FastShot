using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> AddAndUpdateUser(User userObj); 
        Task<User> RegisterUser(User model);
        Task<User?> Authenticate(string? username, string? password);
        Task<User?> GetUserByMobile(string? mobile);
        Task UpdateUser(User user);
        Task<long> GetUserLastNoteId(Guid userId);
        Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId);
    }
}
