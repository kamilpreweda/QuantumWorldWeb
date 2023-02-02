using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class ChangeUserPassword : IRequest
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}