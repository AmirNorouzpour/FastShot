namespace Application.ViewModels
{
    public class AddRawUser
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
}
