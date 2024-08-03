
using Domain.Models;

namespace Application.Interfaces
{
    public interface IRoomRunService
    {
        Task<List<RoomRun>> GetRooms(Guid userId);
    }
}
