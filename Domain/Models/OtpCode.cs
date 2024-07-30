
using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    [Table("OtpCodes")]
    public class OtpCode
    {
        [Key]
        public long Id { get; set; }
        public int Code { get; set; }
        public int SsoType { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public string Receptor { get; set; }
    }
}
