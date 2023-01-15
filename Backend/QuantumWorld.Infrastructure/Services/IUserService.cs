using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string username);

        Task<UserDto> GetAsync(string email);

    }
}