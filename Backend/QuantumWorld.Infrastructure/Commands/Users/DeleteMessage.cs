using MediatR;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Commands.Users
{
    public class DeleteMessage: IRequest
    {
        public int id { get; set; }
        public string email { get; set; }
    }
}