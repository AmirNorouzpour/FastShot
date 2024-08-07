using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddFinanceRecord(Transaction transaction);
        Task<IEnumerable<Transaction>?> GetAll(int page);
        Task<IEnumerable<Transaction>?> GetAll(Guid userId, int page);
        Task<IEnumerable<Transaction>?> GetAll(DateTime start, DateTime end, int page);
    }
}
