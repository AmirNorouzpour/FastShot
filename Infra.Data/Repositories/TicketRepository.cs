using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task AddTicket(Ticket ticket)
        {
            await _Connection.InsertAsync(ticket);
        }

        public async Task AddTicketPost(TicketPost ticketPost)
        {
            await _Connection.InsertAsync(ticketPost);
        }

        public async Task<IEnumerable<Ticket>> GetUserTickets(Guid userId)
        {
            return await _Connection.QueryAsync<Ticket>("SELECT * FROM [Tickets] Where UserId = @userId", new { userId });
        }

        public async Task<IEnumerable<TicketPost>> GetTicketPosts(long ticketId)
        {
            return await _Connection.QueryAsync<TicketPost>("SELECT * FROM [TicketPosts] Where TicketId = @ticketId", new { ticketId });
        }
    }
}
