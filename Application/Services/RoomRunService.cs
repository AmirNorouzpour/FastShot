using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class RoomRunService : IRoomRunService
    {
        private IRoomRepository _repository;

        public RoomRunService(IRoomRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RoomRunGropped>> GetRooms(Guid userId)
        {
            var rooms = await _repository.GetRoomRuns(0);
            var gropedRooms = rooms.GroupBy(x => x.Category).Select(x => new RoomRunGropped { Key = x.FirstOrDefault().CategoryTitle, Items = x.ToList() }).ToList();
            return gropedRooms;
        }

        public async Task<IEnumerable<RoomRunResult>> LastWinners()
        {
            var winners = await _repository.LastWinners();
            return winners;
        }
    }
}
