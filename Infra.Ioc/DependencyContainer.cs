using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //Application Layer
            service.AddScoped<ISecurityService, SecurityService>();
            service.AddTransient<IUserService, UserService>();

            //Infra.Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
