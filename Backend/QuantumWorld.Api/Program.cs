using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Mappers;
using QuantumWorld.Infrastructure.Repositories;
using QuantumWorld.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, InMemoryUserRopository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());

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
