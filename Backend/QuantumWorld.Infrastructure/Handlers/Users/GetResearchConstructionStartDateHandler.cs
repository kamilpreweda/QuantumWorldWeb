using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GetResearchConstructionStartDateHandler : IRequestHandler<GetResearchConstructionStartDate, Unit>
    {
        private readonly IResearchService _researchService;

        public GetResearchConstructionStartDateHandler(IResearchService researchService)
        {
            _researchService = researchService;
        }
        public async Task<Unit> Handle(GetResearchConstructionStartDate request, CancellationToken cancellationToken)
        {
            await _researchService.SetConstructionStartDate(request.type, request.username, request.date);
            return Unit.Value;
        }
    }
}