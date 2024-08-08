
using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IRoomRunService
    {
        Task<List<RoomRunGropped>> GetRooms();
        Task<IEnumerable<RoomRunResult>> LastWinners();
        Task<ApiResult<long>> AddUserToRoom(RoomRunUser roomRunUser, bool check);
        Task<ApiResult<string>> AddTeamToRoom(TeamRegisterModel model);
        Task<RoomRunFlat?> GetRoom(long roomId);
    }
}
