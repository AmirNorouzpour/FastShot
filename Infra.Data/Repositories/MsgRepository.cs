using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class MsgRepository : BaseRepository, IMsgRepository
    {
        public int pageSize = 20;
        public MsgRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AddMsg(Msg msg)
        {
            await _Connection.InsertAsync(msg);
        }

        public Task<IEnumerable<Msg>> GetUserMsgs(Guid userId, int page)
        {
            return _Connection.QueryAsync<Msg>("select * from Msgs Where userId = @userId order by Id desc OFFSET @page * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;", new { userId, page, pageSize });
        }
    }
}
