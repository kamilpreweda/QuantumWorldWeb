using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class ChangeUserPassword : IRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}