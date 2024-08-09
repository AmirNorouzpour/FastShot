using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        Task<ApiResult> AddTicket(Ticket ticket);
        Task<ApiResult> AddTicketPost(TicketPost ticketPost);
        Task<List<Ticket>> GetUserTickets();
        Task<Ticket?> GetTicketById(long ticketId);
        Task<List<TicketPost>> GetTicketPosts(long ticketId);
        Task<ApiResult> AddTicketPostFile(byte[] content, long ticketId, string fileName);
    }
}
