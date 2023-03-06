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
        public async Task UpgradeBuilding(BuildingType type, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var building = user.Buildings.Find(b => b.Type == type);
            user.UpgradeBuilding(type);
            building.ClearConstructionStartDate();
            building.IsBuildingUnderConstruction(false);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }

        public async Task SetConstructionStartDate(BuildingType type, string username, DateTime date)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var building = user.Buildings.Find(b => b.Type == type);
            building.SetConstructionStartDate(date);
            building.IsBuildingUnderConstruction(true);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }

        public async Task CheckConstructionDates(User user)
        {

            DateTime now = DateTime.UtcNow;
            foreach (var building in user.Buildings)
            {
                if (building.ConstructionStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - building.ConstructionStartDate);
                    float timeSpanInSeconds = (float)Math.Round(timeSpan.TotalSeconds);
                    if (timeSpanInSeconds >= building.TimeToBuildInSeconds)
                    {
                        building.ClearConstructionStartDate();
                        user.UpgradeBuilding(building.Type);
                        building.IsBuildingUnderConstruction(false);
                    }
                    else if (timeSpanInSeconds < building.TimeToBuildInSeconds)
                    {
                        bool executedOnce = false;
                        if (!executedOnce)
                        {
                            {
                                building.SetTimeToBuildInSeconds(building.TimeToBuildInSeconds - timeSpanInSeconds);
                                building.SetConstructionStartDate(DateTime.UtcNow);
                                building.IsBuildingUnderConstruction(true);
                            }
                            executedOnce = true;
                        }
                    }
                }
                await Task.CompletedTask;
            }

        }
    }
}
