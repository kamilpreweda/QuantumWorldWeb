using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IBuildingService
    {
        Task UpgradeBuilding(BuildingType type, string email);
    }
}