﻿using Dapper;
using Dapper.Contrib.Extensions;
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

        public async Task<List<int>> GetUserWinsAndPlays(Guid userId)
        {
            var winsCount = await _Connection.ExecuteScalarAsync<int>("SELECT count(*) FROM RoomRunResults where UserId = @userId and IsWinner = 1", new { userId });
            var playsCount = await _Connection.ExecuteScalarAsync<int>("SELECT count(*) FROM RoomRunUsers where UserId = @userId", new { userId });
            return new List<int> { playsCount, winsCount };
        }

        public async Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId)
        {
            return await _Connection.QueryAsync<UserActiveRoomRun>("SELECT rr.Id ,rru.Team,rru.UserId ,rd.Title, rr.StartTime,rr.Status FROM RoomRuns rr LEFT OUTER JOIN RoomDefs rd  ON (rr.RoomDefId = rd.Id)  LEFT OUTER JOIN RoomRunUsers rru ON (rr.Id = rru.RoomRunId)  WHERE UserId = @userId and rr.Status = 0", new { userId });
        }

        public async Task<LeadersBoardResult> GetLeadersBoard(Guid userId)
        {
            var res = new LeadersBoardResult();

            var sql = @"SELECT UserId , COUNT(UserId) Count , (SELECT UserName FROM Users WHERE UserId = UserId) UserName
                        FROM [FastshotDb].[admin].RoomRunResults where IsWinner = 1
                        GROUP BY UserId
                        ORDER BY Count DESC";
            var result = await _Connection.QueryAsync<LeadersBoardResult>(sql, new { userId });
            var result2 = result.ToList();

            var indx = result2.Count < 100 ? result2.Count : 100;
            for (var i = 0; i < indx; i++)
            {
                result2[i].Rank = i + 1;
            }
            res.LeadersBoard = result2;

            var index = result2.FindIndex(x => x.UserId == userId);
            if (index >= 0)
            {
                res.Rank = index + 1;
                res.UserName = result2[index].UserName;
                res.Count = result2[index].Count;
            }
            else
            {
                res.Rank = 1000000;
                res.UserName = "";
                res.Count = 0;
            }

            return res;
        }

        public async Task<UserExtraFieldsModel> GetUserBalance(Guid userId, long roomRunId)
        {
            var res = await _Connection.QueryFirstAsync<UserExtraFieldsModel>("SELECT Credit, (SELECT EntryCostWithOff FROM RoomRuns WHERE Id = @roomRunId) Cost from users where userId = @userId", new { userId, roomRunId });
            return res;
        }

    }
}