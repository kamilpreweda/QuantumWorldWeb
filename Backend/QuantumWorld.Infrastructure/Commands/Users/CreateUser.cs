using MediatR;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class CreateUser : IRequest
    {
        public string Email {get; set;}
        public string Password { get; set; }
        public string Username { get; set; }
    }
}