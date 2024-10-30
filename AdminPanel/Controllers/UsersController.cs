using AdminPanel.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    public class UsersController(IUserService userService, IRoomRunService roomRunService) : BaseController
    {
        public async Task<IActionResult> Index(int? page = 1, int? rows = 20)
        {
            var parameters = getParameters(page, rows);
            var list = await userService.GetAll(parameters);
            ViewBag.TotalRows = await userService.Count(parameters);
            return View(list);
        }

        public async Task<IActionResult> RoomRuns(int? page = 1, int? rows = 20)
        {
            var parameters = getParameters(page, rows);
            var list = await roomRunService.GetAll(parameters);
            ViewBag.TotalRows = await roomRunService.Count(parameters);

            return View(list.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
