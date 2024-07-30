using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class OtpService : IOtpService
    {
        private readonly IOtpCodeRepository _otpCodeRepository;

        public OtpService(IOtpCodeRepository otpCodeRepository)
        {
            _otpCodeRepository = otpCodeRepository;
        }
        public async Task<int> GetCount(string receptor)
        {
            return await _otpCodeRepository.GetOtpsCount(receptor);
        }

        public bool SendEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool SendSms(string receptor)
        {
            var code = new Random((int)DateTime.UtcNow.Ticks).Next(10000, 99999);
            _otpCodeRepository.AddNewCode(new OtpCode
            {
                DateTime = DateTime.UtcNow,
                Code = code,
                ExpireDateTime = DateTime.UtcNow.AddMinutes(3),
                Receptor = receptor,
                SsoType = 0
            });

            return true;
        }
    }
}
