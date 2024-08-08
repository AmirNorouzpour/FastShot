using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

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
        [Authorize(AuthorizeType.Level2)]
        public async Task<ApiResult<List<RoomRunGropped>>> Get()
        {
            var response = await _roomRunsService.GetRooms();
            return new ApiResult<List<RoomRunGropped>> { Success = true, Data = response };
        }

        [HttpGet("[action]/{roomId}")]
        [Authorize(AuthorizeType.Level2)]
        public async Task<ApiResult<RoomRunFlat>> GetRoom(long roomId)
        {
            var response = await _roomRunsService.GetRoom(roomId);
            return new ApiResult<RoomRunFlat> { Success = true, Data = response };
        }

        [HttpGet("[action]/{userId}")]
        [Authorize(AuthorizeType.Level2)]
        public async Task<ApiResult<IEnumerable<RoomRunResult>>> GetLastWinners()
        {
            var response = await _roomRunsService.LastWinners();
            return new ApiResult<IEnumerable<RoomRunResult>> { Success = true, Data = response };
        }

        [HttpPost("addUserRoom")]
        [Authorize(AuthorizeType.Level2)]
        public async Task<ApiResult<long>> AddUserRoom(RoomRunUser model)
        {
            var userId = (Guid?)HttpContext.Items["userId"];
            model.UserId = userId.GetValueOrDefault();
            var response = await _roomRunsService.AddUserToRoom(model, true);
            return response;
        }

        [HttpPost("addTeamRoom")]
        [Authorize(AuthorizeType.Level2)]
        public async Task<ApiResult<string>> AddTeamRoom(TeamRegisterModel model)
        {
            var response = await _roomRunsService.AddTeamToRoom(model);
            return response;
        }
    }
}
