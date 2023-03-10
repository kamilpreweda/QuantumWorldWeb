using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class GetShipConstructionStartDateAndShipCount : IRequest
    {
        public ShipType type { get; set; }
        public string username { get; set; }
        public DateTime date { get; set; }
        public int count { get; set; }
    }
}