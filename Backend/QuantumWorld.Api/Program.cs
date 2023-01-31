using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Mappers;
using QuantumWorld.Infrastructure.Repositories;
using QuantumWorld.Infrastructure.Services;
using MediatR;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, InMemoryUserRopository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IEncrypter, Encrypter>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IResearchService, ResearchService>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IBattleService, BattleService>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
// add logger
// add seedData GeneralSettings
// add IOC container
// builder.Services.AddHttpsRedirection(options =>
//     {
//         options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//         options.HttpsPort = 5001;
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
