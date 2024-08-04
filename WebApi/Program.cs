using Domain.Models;
using Infra.Ioc;
using System.Text.Json.Serialization;
using WebApi.Helpers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull);

        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        DependencyContainer.RegisterServices(builder.Services);


        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<JwtMiddleware>();

        app.Run();
    }
}