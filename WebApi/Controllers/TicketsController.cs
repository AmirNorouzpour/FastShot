using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult> Post(Ticket ticket)
        {
            var response = await _service.AddTicket(ticket);
            return response;
        }

        [HttpPost("[action]")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult> TicketPost(TicketPost ticketPost)
        {
            var response = await _service.AddTicketPost(ticketPost);
            return response;
        }

        [HttpPost("[action]")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<IActionResult> TicketPostFile([FromForm] IFormFile file, long ticketId)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var response = await _service.AddTicketPostFile(fileBytes, ticketId, file.FileName);
                return Ok(response);
            }
        }

        [HttpGet]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<Ticket>>> Get()
        {
            var response = await _service.GetUserTickets();
            return new ApiResult<List<Ticket>> { Success = true, Data = response };
        }

        [HttpGet("[action]")]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<TicketPost>>> GetTicketPosts(long ticketId)
        {
            var response = await _service.GetTicketPosts(ticketId);
            return new ApiResult<List<TicketPost>> { Success = true, Data = response };
        }
    }
}
