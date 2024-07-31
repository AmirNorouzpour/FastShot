using Domain.Models;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> AddAndUpdateUser(User userObj);
        Task<ApiResult> RegisterUser(RegisterUserModel model);
        Task<ApiResult<Guid>> VerifyUser(SsoVerifyModel model);
    }
}
