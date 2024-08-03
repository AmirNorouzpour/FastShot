using Application.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class RoomRunService : IRoomRunService
    {
        public Task<List<RoomRun>> GetRooms(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
