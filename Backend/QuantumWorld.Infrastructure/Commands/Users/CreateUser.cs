using MediatR;
using MongoDB.Bson;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class CreateUser : IRequest
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}