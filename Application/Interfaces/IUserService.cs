using Domain.Models;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateReq model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> AddAndUpdateUser(User userObj);
        Task<ApiResult> RegisterUser(RegisterUserModel model);
        Task<ApiResult<Guid>> VerifyUser(SsoVerifyModel model);
        Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId);
        Task<long> GetUserLastNoteId(Guid userId);
        Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId);
    }
}
