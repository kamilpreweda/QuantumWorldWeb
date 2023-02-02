using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Mappers;
using QuantumWorld.Infrastructure.Repositories;
using QuantumWorld.Infrastructure.Services;
using QuantumWorld.Infrastructure.Mongo;
using MediatR;
using System.Net;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(name: "UserOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
// builder.Services.AddScoped<IUserRepository, InMemoryUserRopository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IEncrypter, Encrypter>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IResearchService, ResearchService>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IBattleService, BattleService>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("mongo"));
MongoConfiguration.Initialize();
builder.Services.AddScoped<IDbConnection, DbConnection>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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


app.UseCors("UserOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
