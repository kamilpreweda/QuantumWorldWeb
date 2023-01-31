using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task RegisterAsync(Guid userId, string email, string password, string username);
        Task LoginAsync(string email, string password);       
     



    }
}