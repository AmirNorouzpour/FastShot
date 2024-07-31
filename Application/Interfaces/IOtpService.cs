namespace Application.Interfaces
{
    public interface IOtpService
    {
        Task<bool> SendSms(string mobile);
        bool SendEmail(string email);
        Task<int> GetCount(string receptor);
        Task<bool> CheckCode(string receptor, string code, int ssoType);
    }
}
