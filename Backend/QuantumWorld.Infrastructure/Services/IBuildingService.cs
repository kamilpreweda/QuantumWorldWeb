using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IBuildingService
    {
        Task UpgradeBuilding(BuildingType type, string email);

        Task SetConstructionStartDate(BuildingType type, string username, DateTime date);

        Task CheckConstructionDates(User user);
    }
}