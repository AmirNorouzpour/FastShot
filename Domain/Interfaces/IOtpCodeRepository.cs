using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOtpCodeRepository
    {
        Task<int> GetOtpsCount(string recptor);
        Task<OtpCode?> AddNewCode(OtpCode obj);
        Task<OtpCode?> GetOtpByCode(string receptor, int ssoType);
        Task UpdateOtpCode(OtpCode otpCode);
    }
}
