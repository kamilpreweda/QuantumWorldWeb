using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class DeleteUser : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}