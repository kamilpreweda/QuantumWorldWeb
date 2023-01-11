using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        void Register(string email, string password, string username);

        UserDto GetUser(string email);

    }
}