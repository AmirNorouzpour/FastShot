using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOtpCodeRepository
    {
        Task<int> GetOtpsCount(string recptor);
        Task<OtpCode?> AddNewCode(OtpCode obj);
    }
}
