using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public int Side { get; set; }
        public int Type { get; set; }
        public string Desc { get; set; }
        public Guid CreatorId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
