using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public TicketStatus Status { get; set; }
        public Guid UserId { get; set; }
    }

    [Table("TicketPosts")]
    public class TicketPost
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public DateTime DateTime { get; set; }
        public string Msg { get; set; }
        public byte[]? File { get; set; }
        public string? AdminName { get; set; }
        public string? UserName { get; set; }
        public int FileLength { get; set; }
        public string? FileName { get; set; }
    }
}
