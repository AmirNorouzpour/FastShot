using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public int pageSize = 20;
        public TransactionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Transaction>?> GetAll(int page)
        {
            return await _Connection.QueryAsync<Transaction>("SELECT * FROM [Transactions] order by Id desc OFFSET @page * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;", new { page, pageSize });
        }

        public async Task<IEnumerable<Transaction>?> GetAll(Guid userId, int page)
        {
            return await _Connection.QueryAsync<Transaction>("SELECT * FROM [Transactions] Where userId=@userId order by Id desc OFFSET @page * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;", new { userId, page, pageSize });
        }

        public async Task<IEnumerable<Transaction>?> GetAll(DateTime start, DateTime end, int page)
        {
            return await _Connection.QueryAsync<Transaction>("SELECT * FROM [Transactions] Where DateTime >= @start and DateTime <= @end order by Id desc OFFSET @page * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;", new { start, end, page, pageSize });
        }
    }
}
