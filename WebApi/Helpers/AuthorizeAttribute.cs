using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute(AuthorizeType type = AuthorizeType.Level1)
        {
            Type = type;
        }
        public AuthorizeType Type { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var lvl1 = (string?)context.HttpContext.Request.Headers["fixed"];
            if (string.IsNullOrWhiteSpace(lvl1) || lvl1 != "ccwHm85PKMOGMcqDnXzmPgTbHNQT3E2Yn")
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var userId = (Guid?)context.HttpContext.Items["userId"];
            if (userId == null && Type == AuthorizeType.Level2)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
