using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> AddAndUpdateUser(User userObj); 
        Task<User?> Authenticate(string username, string password);
    }
}
