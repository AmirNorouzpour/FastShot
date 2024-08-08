using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddTicket(Ticket ticket);
        Task AddTicketPost(TicketPost ticketPost);
        Task<IEnumerable<Ticket>> GetUserTickets(Guid userId);
        Task<IEnumerable<TicketPost>> GetTicketPosts(long ticketId);
    }
}
