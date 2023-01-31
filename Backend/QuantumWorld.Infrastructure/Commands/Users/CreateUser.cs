using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class CreateUser : IRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}