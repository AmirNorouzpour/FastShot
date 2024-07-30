namespace Application.Interfaces
{
    public interface IOtpService
    {
        bool SendSms(string mobile);
        bool SendEmail(string email);
        Task<int> GetCount(string receptor);
    }
}
