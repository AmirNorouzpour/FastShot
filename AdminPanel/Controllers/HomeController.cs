using AdminPanel.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int? page = 1, int? rows = 3)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("page", page.GetValueOrDefault() - 1);
            dic.Add("rows", rows.GetValueOrDefault());
            foreach (var item in Request.Query)
            {
                if (item.Key != "page" && item.Key != "sort" && item.Key != "rows")
                    dic.Add(item.Key.Split("-")[0], item.Value);
            }
            var list = await _userService.GetAll(dic);
            ViewBag.TotalRows = await _userService.Count(dic); ;
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
