using Domain.Models;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateReq model);
        Task<User?> GetById(Guid id);
        Task<IEnumerable<User>> GetAll(Dictionary<string, object> dictionary);
        Task<int> Count(Dictionary<string, object> dictionary);
        Task<ApiResult> RegisterUser(RegisterUserModel model);
        Task<ApiResult<AuthenticateResponse>> VerifyUser(SsoVerifyModel model);
        Task<ApiResult<UserInfoModel>> GetUserInfo(Guid userId);
        Task<long> GetUserLastNoteId(Guid userId);
        Task<IEnumerable<UserActiveRoomRun>> GetUserActiveRoomRuns(Guid userId);
        Task<LeadersBoardResult> GetLeadersBoard(Guid userId);
        Task<UserExtraFieldsModel> GetUserBalance(Guid userId, long roomRunId);
        Task<ApiResult<string>> UpdateUsername(string? username);
        Task<ApiResult<string>> UpdateSheba(string? username);
    }
}
