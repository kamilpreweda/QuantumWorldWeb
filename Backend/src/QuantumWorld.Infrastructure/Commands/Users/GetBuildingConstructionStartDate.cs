using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class GetBuildingConstructionStartDate: IRequest
    {
        public BuildingType type { get; set; }
        public string username { get; set; }
        public DateTime date { get; set; }
    }
}