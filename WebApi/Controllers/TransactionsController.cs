using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Type = AuthorizeType.Level2)]
        public async Task<ApiResult<List<Transaction>>> Get(int page)
        {
            var userId = (Guid?)HttpContext.Items["userId"];
            var response = await _service.GetAll(userId.GetValueOrDefault(), page);
            return response;
        }
    }
}
