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
        private IRoomRunService _service;

        public RoomsController(IRoomRunService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResult<List<RoomRun>>> Get(Guid userId)
        {
            var response = await _service.GetRooms(userId);

            if (response == null)
                return new ApiResult<List<RoomRun>> { Success = false, Msg = "رقابتی وجود ندارد!" };

            return new ApiResult<List<RoomRun>> { Success = true, Data = response };
        }
    }
}
