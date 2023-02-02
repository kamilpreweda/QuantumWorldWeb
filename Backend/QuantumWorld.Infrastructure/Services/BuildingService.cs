using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IUserRepository _userRepository;
        public BuildingService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task UpgradeBuilding(BuildingType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is null)
            {
                throw new Exception($"User with {email} doesn't exist!");
            }
            user.UpgradeBuilding(type);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;

        }

    }
}