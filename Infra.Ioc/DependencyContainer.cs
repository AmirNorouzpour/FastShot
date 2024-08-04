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
            service.AddTransient<IRoomRunService, RoomRunService>();
            service.AddTransient<IOtpService, OtpService>();

            //Infra.Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IRoomRepository, RoomRepository>();
            service.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
        }
    }
}
