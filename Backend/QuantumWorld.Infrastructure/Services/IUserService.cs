using MongoDB.Bson;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid id, string email, string password, string username);
        Task LoginAsync(string email, string password);
        Task DeleteAsync(string email, string password);




    }
}