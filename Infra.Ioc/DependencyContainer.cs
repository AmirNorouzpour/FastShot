using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
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
            service.AddScoped<ITransactionService, TransactionService>();
            service.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


            //Infra.Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IRoomRepository, RoomRepository>();
            service.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
            service.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
