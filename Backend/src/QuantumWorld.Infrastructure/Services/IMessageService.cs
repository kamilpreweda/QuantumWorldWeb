using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IMessageService
    {
         Task DeleteAsync(int id, string email);
    }
}