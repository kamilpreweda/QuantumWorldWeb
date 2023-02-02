using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class BuildShip : IRequest
    {
        public ShipType type { get; set; }
        public string email { get; set; } = string.Empty;
    }
}