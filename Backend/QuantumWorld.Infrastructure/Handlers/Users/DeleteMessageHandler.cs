using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class DeleteMessageHandler: IRequestHandler<DeleteMessage, Unit>
    {
        private readonly IMessageService _messageService;

        public DeleteMessageHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<Unit> Handle(DeleteMessage request, CancellationToken cancellationToken)
        {
            await _messageService.DeleteAsync(request.id, request.email);
            return Unit.Value;
        }
    }
}