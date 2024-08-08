using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    [Table("Friends")]
    public class Friend
    {
        public long Id { get; set; }
        public Guid UserId1 { get; set; }
        [Computed]
        public string? UserName1 { get; set; }
        [Computed]
        public string? UserName2 { get; set; }
        public Guid UserId2 { get; set; }
        public bool? Confrimed { get; set; }
        public DateTime DateTime { get; set; }
    }
}
