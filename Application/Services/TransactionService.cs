using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task AddFinanceRecord(decimal cost, FinanceSide side, FinanceType type, Guid userId, Guid creator, FinanceStatus status, string desc)
        {
            await _repository.AddFinanceRecord(new Transaction
            {
                Amount = cost,
                CreatorId = creator,
                DateTime = DateTime.UtcNow,
                Desc = desc,
                IsDeleted = false,
                Side = side,
                Type = type,
                UserId = userId,
                Status = status
            });
        }

        public async Task<ApiResult<List<Transaction>>?> GetAll(int page)
        {
            var res = await _repository.GetAll(page);
            return new ApiResult<List<Transaction>> { Success = true, Data = res?.ToList() };
        }

        public async Task<ApiResult<List<Transaction>>> GetAll(Guid userId, int page)
        {
            var res = await _repository.GetAll(userId, page);
            return new ApiResult<List<Transaction>> { Success = true, Data = res?.ToList() };
        }

        public async Task<ApiResult<List<Transaction>>> GetAll(DateTime start, DateTime end, int page)
        {
            var res = await _repository.GetAll(start, end, page);
            return new ApiResult<List<Transaction>> { Success = true, Data = res?.ToList() };
        }
    }
}
