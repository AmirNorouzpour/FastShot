using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<RoomRunFlat>> GetRoomRuns(int status)
        {
            return await _Connection.QueryAsync<RoomRunFlat>("SELECT rr.Id ,rd.Title,rd.EntryCost,rd.EntryCostWithOff, rr.StartTime,rr.Status, rd.Capacity,rd.Category,rd.CategoryTitle, (SELECT COUNT(*) FROM RoomRunUsers rru WHERE rru.RoomRunId = rr.Id) UsersCount FROM RoomRuns rr  JOIN RoomDefs rd  ON (rr.RoomDefId = rd.Id)  WHERE rr.Status = @status", new { status });
        }

        public async Task<IEnumerable<RoomRunResult>> LastWinners()
        {
            return await _Connection.QueryAsync<RoomRunResult>("SELECT TOP (30) rrr.Id,[RoomRunId],(SELECT UserName FROM Users WHERE UserId = rrr.UserId) UserName,[Amount] FROM [RoomRunResults] rrr where iswinner = 1 order by id desc");
        }

        public async Task<long> AddUserToRoom(RoomRunUser roomRunUser)
        {
            var res = await _Connection.InsertAsync(roomRunUser);
            return roomRunUser.Id;
        }

        public async Task<bool> CheckUserInRoom(Guid userId, long roomRunId)
        {
            var count = await _Connection.ExecuteScalarAsync<int>("SELECT count(userId) FROM [RoomRunUsers] where userId = @userId and RoomRunId = @roomRunId", new { userId, roomRunId });
            return count > 0;
        }

        public async Task<RoomRunFlat?> GetRoom(long roomRunId)
        {
            return await _Connection.QueryFirstOrDefaultAsync<RoomRunFlat>("SELECT rr.Id, rr.Status, rr.StartTime, rr.EntryCost, rr.EntryCostWithOff, rd.Capacity, rd.TeamsUsersCount,rd.CategoryTitle,rd.[Desc],rd.Title,  (SELECT COUNT(*) FROM RoomRunUsers rru WHERE rru.RoomRunId = rr.Id) UsersCount FROM [RoomRuns] rr join RoomDefs rd on (rr.RoomDefId = rd.Id) where rr.Id = @roomRunId", new { roomRunId });
        }

        public async Task<IEnumerable<UserTeamModel>> GetRoomRunUsers(long roomRunId)
        {
            return await _Connection.QueryAsync<UserTeamModel>("SELECT [UserId],[Team] FROM [RoomRunUsers] where RoomRunId = @roomRunId", new { roomRunId });
        }
    }
}
