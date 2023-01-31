using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class UpgradeResearchHandler : IRequestHandler<UpgradeResearch, Unit>
    {
        private readonly IResearchService _researchService;

        public UpgradeResearchHandler(IResearchService researchService)
        {
            _researchService = researchService;
        }
        public async Task<Unit> Handle(UpgradeResearch request, CancellationToken cancellationToken)
        {
            _researchService.UpgradeResearch(request.type, request.email);
            return Unit.Value;
        }
    }
}