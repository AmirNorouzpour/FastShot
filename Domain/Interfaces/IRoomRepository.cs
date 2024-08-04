using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomRunFlat>> GetRoomRuns(int status);
        Task<IEnumerable<RoomRunResult>> LastWinners();
    }
}
