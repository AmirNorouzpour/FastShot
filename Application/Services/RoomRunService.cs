using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using System.Reflection;

namespace Application.Services
{
    public class RoomRunService : IRoomRunService
    {
        private IRoomRepository _repository;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public RoomRunService(IRoomRepository repository, IUserService userService, ITransactionService transactionService)
        {
            _repository = repository;
            _userService = userService;
            _transactionService = transactionService;
        }
        public async Task<List<RoomRunGropped>> GetRooms(Guid userId)
        {
            var rooms = await _repository.GetRoomRuns(0);
            var gropedRooms = rooms.GroupBy(x => x.Category).Select(x => new RoomRunGropped { Key = x.FirstOrDefault().CategoryTitle, Items = x.ToList() }).ToList();
            return gropedRooms;
        }

        public async Task<IEnumerable<RoomRunResult>> LastWinners()
        {
            var winners = await _repository.LastWinners();
            return winners;
        }

        public async Task<ApiResult<long>> AddUserToRoom(RoomRunUser roomRunUser, bool check = true)
        {
            decimal cost = 0;
            var userData = await _userService.GetUserBalance(roomRunUser.UserId, roomRunUser.RoomRunId);
            cost = userData.Cost;

            if (check)
            {
                var checkroom = await _repository.CheckUserInRoom(roomRunUser.UserId, roomRunUser.RoomRunId);
                if (!checkroom)
                    return new ApiResult<long> { Msg = "شما قبلا در این رقابت ثبت نام کرده اید" };

                if (userData.Credit < userData.Cost)
                    return new ApiResult<long> { Msg = "موجودی کافی برای شرکت در رقابت ندارید" };
            }

            await _transactionService.AddFinanceRecord(cost, FinanceSide.Up, FinanceType.SherkatDarRoom, roomRunUser.UserId, roomRunUser.UserId, "desc");

            roomRunUser.DateTime = DateTime.UtcNow;
            var res = await _repository.AddUserToRoom(roomRunUser);
            return new ApiResult<long> { Success = true, Data = res };
        }

        public async Task<ApiResult<string>> AddTeamToRoom(TeamRegisterModel model)
        {
            foreach (var item in model.Users)
            {
                var check = await _repository.CheckUserInRoom(item.UserId, model.RoomRunId);
                if (!check)
                    return new ApiResult<string> { Msg = $"{item.UserName} قبلا در این رقابت شرکت کرده است" };
            }

            foreach (var item in model.Users)
            {
                var userData = await _userService.GetUserBalance(item.UserId, model.RoomRunId);
                if (userData.Credit < userData.Cost)
                    return new ApiResult<string> { Msg = $"{item.UserName} موجودی کافی برای شرکت در رقابت ندارد" };
            }

            foreach (var item in model.Users)
            {
                await AddUserToRoom(new RoomRunUser { UserId = item.UserId, RoomRunId = model.RoomRunId, Team = model.Team }, false);
            }

            return new ApiResult<string> { Success = true, Data = model.Team };
        }

        public async Task<RoomRunFlat?> GetRoom(long roomId)
        {
            var room = await _repository.GetRoom(roomId);
            if (room != null)
            {
                var users = await _repository.GetRoomRunUsers(roomId);
                room.Users = users.ToList();
            }
            return room;
        }
    }
}
