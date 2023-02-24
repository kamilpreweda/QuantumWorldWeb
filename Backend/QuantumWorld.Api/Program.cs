using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Mappers;
using QuantumWorld.Infrastructure.Repositories;
using QuantumWorld.Infrastructure.Services;
using QuantumWorld.Infrastructure.Mongo;
using MediatR;
using System.Net;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false        
    };
});
builder.Services.AddCors(options => options.AddPolicy(name: "UserOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
// builder.Services.AddScoped<IUserRepository, InMemoryUserRopository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IEncrypter, Encrypter>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IResearchService, ResearchService>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IBattleService, BattleService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddHttpContextAccessor();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
