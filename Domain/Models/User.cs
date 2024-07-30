
namespace Domain.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public decimal Credit { get; set; }
        public decimal RealCredit { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public int WinCount { get; set; }
        public int GameCount { get; set; }
        public string DeviceName { get; set; }
        public string DeviceUid { get; set; }
        public int FreeUsedCount { get; set; }
        public string? PasswordHash { get; set; }
        public int Role { get; set; }
    }
}
