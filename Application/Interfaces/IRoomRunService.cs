
using Domain.Models;

namespace Application.Interfaces
{
    public interface IRoomRunService
    {
        Task<List<RoomRunGropped>> GetRooms(Guid userId);
        Task<IEnumerable<RoomRunResult>> LastWinners();
    }
}
