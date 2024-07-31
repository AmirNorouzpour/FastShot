using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

        public async Task<User> AddRawUser(User model)
        {
            var res = await _Connection.InsertAsync(model);
            return model;
        }

        public async Task<User?> GetUserByMobile(string? mobile)
        {
            return await _Connection.QueryFirstOrDefaultAsync<User?>("select * from users where mobile = @mobile", new { mobile });
        }

        public async Task UpdateUser(User user)
        {
            await _Connection.UpdateAsync(user);
        }
    }
}
