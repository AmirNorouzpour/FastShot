using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> getParameters(int? page, int? rows)
        {
            var parameters = new Dictionary<string, object>
            {
                { "page", page.GetValueOrDefault() - 1 },
                { "rows", rows.GetValueOrDefault() }
            };
            foreach (var item in Request.Query)
            {
                if (item.Key != "page" && item.Key != "sort" && item.Key != "rows")
                    parameters.Add(item.Key.Split("-")[0], item.Value);
            }
            return parameters;
        }
    }
}