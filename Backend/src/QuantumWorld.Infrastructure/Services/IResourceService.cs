using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IResourceService
    {
         List<Resource> GetUserResources(User user);
    }
}