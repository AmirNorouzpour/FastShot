using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        Task<ApiResult> AddTicket(Ticket ticket);
        Task<ApiResult> AddTicketPost(TicketPost ticketPost);
        Task<List<Ticket>> GetUserTickets();
        Task<List<TicketPost>> GetTicketPosts(long ticketId);
    }
}
