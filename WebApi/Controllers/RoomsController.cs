using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRoomRunService _roomRunsService;

        public RoomsController(IRoomRunService roomRunService)
        {
            _roomRunsService = roomRunService;
        }

        [HttpGet]
        public async Task<ApiResult<List<RoomRunGropped>>> Get(Guid userId)
        {
            var response = await _roomRunsService.GetRooms(userId);
            return new ApiResult<List<RoomRunGropped>> { Success = true, Data = response };
        }

        [HttpGet("[action]/{roomId}")]
        public async Task<ApiResult<RoomRunFlat>> GetRoom(long roomId)
        {
            var response = await _roomRunsService.GetRoom(roomId);
            return new ApiResult<RoomRunFlat> { Success = true, Data = response };
        }

        [HttpGet("[action]/{userId}")]
        public async Task<ApiResult<IEnumerable<RoomRunResult>>> GetLastWinners(Guid userId)
        {
            var response = await _roomRunsService.LastWinners();
            return new ApiResult<IEnumerable<RoomRunResult>> { Success = true, Data = response };
        }

        [HttpPost("addUserRoom")]
        public async Task<ApiResult<long>> AddUserRoom(RoomRunUser model)
        {
            var response = await _roomRunsService.AddUserToRoom(model, true);
            return response;
        }

        [HttpPost("addTeamRoom")]
        public async Task<ApiResult<string>> AddTeamRoom(TeamRegisterModel model)
        {
            var response = await _roomRunsService.AddTeamToRoom(model);
            return response;
        } 

    }
}
