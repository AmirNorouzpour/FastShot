using Dapper;
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
            return await _Connection.QueryAsync<RoomRunFlat>("SELECT rr.Id ,rd.Title,rd.EntryCost,rd.EntryCostWithOff, rr.StartTime,rr.Status, rd.Capacity,rd.Category,rd.CategoryTitle, (SELECT COUNT(*) FROM RoomRunUsers rru WHERE rru.RoomRunId = rr.Id) UsersCount FROM RoomRuns rr\r\nJOIN RoomDefs rd  ON (rr.RoomDefId = rd.Id)\r\nWHERE rr.Status = @status", new { status });
        }

        public async Task<IEnumerable<RoomRunResult>> LastWinners()
        {
            return await _Connection.QueryAsync<RoomRunResult>("SELECT TOP (30) rrr.Id,[RoomRunId],(SELECT UserName FROM Users WHERE UserId = rrr.UserId) UserName,[Amount] FROM [RoomRunResults] rrr where iswinner = 1 order by id desc");
        }
    }
}
