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

        public async Task<User?> Authenticate(string? username, string? password)
        {
            return await _Connection.QueryFirstOrDefaultAsync<User?>("select id from users where username = @username and PasswordHash = @password", new { username, password });
        }

        public async Task<User> RegisterUser(User model)
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

        public async Task<User?> GetById(Guid id)
        {
            return await _Connection.QueryFirstOrDefaultAsync<User?>("select * from users where id = @id", new { id });
        }

        public async Task<long> GetUserLastNoteId(Guid userId)
        {
            var id = await _Connection.ExecuteScalarAsync<long>("select top 1 id from Msgs where userId = @userId order by datetime desc", new { userId });
            return id;
        }

        public async Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId)
        {
            return await _Connection.QueryAsync<UserActiveRoomRun>("SELECT rr.Id ,rru.Team,rru.PlayerId ,rd.Title, rr.StartTime,rr.Status FROM RoomRuns rr\r\nLEFT OUTER JOIN RoomDefs rd  ON (rr.RoomDefId = rd.Id)\r\nLEFT OUTER JOIN RoomRunUsers rru ON (rr.Id = rru.RoomRunId)\r\nWHERE PlayerId = @userId and rr.Status = 0", new { userId });
        }

    }
}
