using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class Login : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}