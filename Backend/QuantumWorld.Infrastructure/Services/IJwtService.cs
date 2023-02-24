using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IJwtService
    {
         JwtDto CreateToken(string username);
    }
}