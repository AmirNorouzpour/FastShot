using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Repositories;
using Infra.Ioc;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
DependencyContainer.RegisterServices(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();

