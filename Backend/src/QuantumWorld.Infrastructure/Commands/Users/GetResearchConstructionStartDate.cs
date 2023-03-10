using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class GetResearchConstructionStartDate : IRequest
    {
        public ResearchType type { get; set; }
        public string username { get; set; }
        public DateTime date { get; set; }
    }
}