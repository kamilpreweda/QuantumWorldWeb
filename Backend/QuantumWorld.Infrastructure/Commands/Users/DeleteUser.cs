using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class DeleteUser : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}