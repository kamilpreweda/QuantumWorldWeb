using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class StartBattle : IRequest
    {
        public EnemyType type { get; set; }
        public string email { get; set; }
    }
}