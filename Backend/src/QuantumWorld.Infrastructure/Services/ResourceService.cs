using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public class ResourceService : IResourceService
    {
        public List<Resource> GetUserResources(User user)
        {
            var resources = user.GetResources();
            return resources;
        }
    }
}