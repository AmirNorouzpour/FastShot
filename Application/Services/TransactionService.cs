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
