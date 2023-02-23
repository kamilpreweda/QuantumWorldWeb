using MongoDB.Bson;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string username);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(string password, string username);
        Task LoginAsync(string username, string password);
        Task DeleteAsync(string username, string password);




    }
}