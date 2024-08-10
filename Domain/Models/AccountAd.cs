using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    [Table("AccountAds")]
    public class AccountAd
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Region { get; set; }
        public string Uid { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public string VasetName { get; set; }
        public string UserTelegramId { get; set; }
        public string UserMobileNo { get; set; }
        public int Status { get; set; }
        public string Links { get; set; }
    }

    [Table("AccountAdFiles")]
    public class AccountAdFile
    {
        public long Id { get; set; }
        public long AccountAdId { get; set; }
        public byte[]? File { get; set; }
        public int FileLength { get; set; }
        public string? FileName { get; set; }
    }

    [Table("AccountAdsDetails")]
    public class AccountAdsDetail
    {
        public long Id { get; set; }
        public long AccountAdId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public int Rate { get; set; }
    }
}
