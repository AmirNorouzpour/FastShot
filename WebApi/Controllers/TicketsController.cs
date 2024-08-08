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

        [HttpGet]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<Ticket>>> Get()
        {
            var response = await _service.GetUserTickets();
            return new ApiResult<List<Ticket>> { Success = true, Data = response };
        }

        [HttpGet]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<TicketPost>>> GetTicketPosts(long ticketId)
        {
            var response = await _service.GetTicketPosts(ticketId);
            return new ApiResult<List<Ticket>> { Success = true, Data = response };
        }
    }
}
