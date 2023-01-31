using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IUserRepository _userRepository;
        public async Task UpgradeBuilding(BuildingType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            user.UpgradeBuilding(type);
            await Task.CompletedTask;

        }

    }
}