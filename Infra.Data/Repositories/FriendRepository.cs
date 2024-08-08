using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public int pageSize = 20;
        public FriendRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AcceptFriendRequest(Guid userId2, bool status)
        {
            await _Connection.QueryAsync<Friend>("Update [Friends] set Confrimed = @status Where userId2 = @userId", new { status, userId2 });
        }

        public async Task AddFriendRequest(Friend friend)
        {
            await _Connection.InsertAsync(friend);
        }

        public async Task<IEnumerable<Friend>> GetFriends(Guid userId)
        {
            return await _Connection.QueryAsync<Friend>("SELECT  Id, UserId1,UserId2, Confrimed, DateTime, (SELECT UserName FROM Users u WHERE f.UserId1 = u.Id) UserName1 , (SELECT UserName FROM Users u WHERE f.UserId2 = u.Id) UserName2 FROM [Friends] Where (userId1 = @userId or userId2 = @userId) and Confrimed != 0 order by Id desc", new { userId });
        }

        public async Task<int> ReqCount(Guid userId, Guid userId2)
        {
            return await _Connection.ExecuteScalarAsync<int>("SELECT count(*) FROM [Friends] Where (UserId2 = @userId2 and UserId1 = @userId) and Confrimed != 0", new { userId, userId2 });
        }
    }
}
