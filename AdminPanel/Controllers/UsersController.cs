using AdminPanel.Models;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoomRunService _roomRunService;

        public UsersController(IUserService userService, IRoomRunService roomRunService)
        {
            _userService = userService;
            _roomRunService = roomRunService;
        }

        public async Task<IActionResult> Index(int? page = 1, int? rows = 20)
        {
            var parameters = getParameters(page, rows);
            var list = await _userService.GetAll(parameters);
            ViewBag.TotalRows = await _userService.Count(parameters);
            return View(list);
        }

        public async Task<IActionResult> RoomRuns(int? page = 1, int? rows = 20)
        {
            var parameters = getParameters(page, rows);
            var list = await _roomRunService.GetAll(parameters);
            ViewBag.TotalRows = await _roomRunService.Count(parameters);

            var list2 = new List<RoomRunFlat> { new RoomRunFlat { Text = "asdsadsad" } };
            return View(list2.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
