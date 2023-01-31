using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class UpgradeBuilding : IRequest
    {
        public BuildingType type { get; set; }
        public string email { get; set; }
    }
}