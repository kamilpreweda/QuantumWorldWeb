using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IUserService
    {
        void Register(string email, string password);

    }
}