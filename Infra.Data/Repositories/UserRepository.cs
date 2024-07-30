using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

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

        public async Task<User?> Authenticate(string? username, string? password)
        {
            return await _Connection.QueryFirstOrDefaultAsync<User?>("select id from users where username = @username and PasswordHash = @password", new { username, password });
        }
    }
}
