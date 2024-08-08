using Dapper.Contrib.Extensions;


namespace Domain.Models
{
    [Table("Msgs")]
    public class Msg
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
        public MsgType MsgType { get; set; }
    }
}

