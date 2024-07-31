using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using System.Text.Json;

namespace Application.Services
{
    public class OtpService : IOtpService
    {
        private readonly IOtpCodeRepository _otpCodeRepository;

        public OtpService(IOtpCodeRepository otpCodeRepository)
        {
            _otpCodeRepository = otpCodeRepository;
        }

        public async Task<bool> CheckCode(string receptor, string code, int ssoType)
        {
            var otpCode = await _otpCodeRepository.GetOtpByCode(receptor, ssoType);
            if (otpCode?.Code == int.Parse(code) && otpCode.ExpireDateTime > DateTime.UtcNow)
            {
                otpCode.ExpireDateTime = DateTime.UtcNow;
                await _otpCodeRepository.UpdateOtpCode(otpCode);
                return true;
            }
            return false;
        }

        public async Task<int> GetCount(string receptor)
        {
            return await _otpCodeRepository.GetOtpsCount(receptor);
        }

        public bool SendEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendSms(string receptor)
        {
            var code = new Random((int)DateTime.UtcNow.Ticks).Next(10000, 99999);
            var sentRes = await Sms(receptor, code.ToString());
            if (sentRes)
            {
                await _otpCodeRepository.AddNewCode(new OtpCode
                {
                    DateTime = DateTime.UtcNow,
                    Code = code,
                    ExpireDateTime = DateTime.UtcNow.AddMinutes(3),
                    Receptor = receptor,
                    SsoType = 0
                });

                return true;
            }
            return false;
        }

        private async Task<bool> Sms(string receptor, string token, string template = "fastOtp")
        {
            try
            {
                var postData = new Dictionary<string, string>();
                postData.Add("receptor", receptor);
                postData.Add("token", token);
                postData.Add("template", template);

                var url = "https://api.kavenegar.com/v1/663053314354586A526E42616B556C51536B64775236466E6A4E596F37304730/verify/lookup.json";

                using (var httpClient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(postData))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                        HttpResponseMessage response = await httpClient.PostAsync(url, content);

                        var res = await response.Content.ReadAsStringAsync();
                        var obj = JsonSerializer.Deserialize<OtpReturnRes>(res);
                        if (obj?.@return?.status == 200)
                            return true;

                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
