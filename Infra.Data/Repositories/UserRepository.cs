using Domain.Interfaces;
using Domain.Models;

namespace Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> AddAndUpdateUser(User userObj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
