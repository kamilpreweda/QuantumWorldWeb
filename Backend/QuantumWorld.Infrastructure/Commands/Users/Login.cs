using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class Login : IRequest
    {
        public Guid TokenId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}