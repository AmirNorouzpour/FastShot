using Domain.Models;

namespace Application.ViewModels
{
    public class RegisterUserModel
    {
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceUid { get; set; }
        public int SsoType { get; set; }
        public Guid Id { get; set; }
    }

    public class SsoVerifyModel
    {
        public string Receptor { get; set; }
        public string Code { get; set; }
        public int SsoType { get; set; }

    }

    public class UserInfoModel
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public long LastNoteId { get; set; }
        public string? UserName { get; set; }
        public List<UserActiveRoomRun>? ActiveRoomRuns { get; set; }
        public object WinsCount { get; set; }
        public object PlaysCount { get; set; }
    }
}
