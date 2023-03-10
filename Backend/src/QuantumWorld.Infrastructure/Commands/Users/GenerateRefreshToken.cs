using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class GenerateRefreshToken : IRequest
    {
        public Guid TokenId { get; set; }
        // public RefreshToken RefreshToken { get; set; }
        public string Username { get; set; }
    }
}