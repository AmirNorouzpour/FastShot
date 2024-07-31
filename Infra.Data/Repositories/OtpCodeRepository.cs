using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class OtpCodeRepository : BaseRepository, IOtpCodeRepository
    {
        public OtpCodeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<OtpCode?> AddNewCode(OtpCode obj)
        {
            var res = await _Connection.InsertAsync(obj);
            return obj;
        }

        public async Task<OtpCode?> GetOtpByCode(string receptor, int ssoType)
        {
            var res = await _Connection.QueryFirstAsync<OtpCode>("select top 1 * from otpcodes where receptor = @receptor and ssoType = @ssoType order by DateTime desc", new { receptor, ssoType });
            return res;
        }

        public async Task<int> GetOtpsCount(string receptor)
        {
            var count = await _Connection.ExecuteScalarAsync<int>("select count(*) from otpcodes where receptor = @receptor and [datetime] > @date", new { receptor, date = DateTime.UtcNow.AddMinutes(-30) });
            return count;
        }

        public async Task UpdateOtpCode(OtpCode otpCode)
        {
            await _Connection.UpdateAsync(otpCode);
        }
    }
}
