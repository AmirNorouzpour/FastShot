﻿using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ApiResult<List<Transaction>>> GetAll(int page);
        Task<ApiResult<List<Transaction>>> GetAll(Guid userId, int page);
        Task<ApiResult<List<Transaction>>> GetAll(DateTime start, DateTime end, int page);
    }
}