using QuantumWorld.Core.Domain;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IJwtService
    {
         JwtDto CreateToken(string username);
         RefreshToken GenerateRefreshToken();
         void SetRefreshToken(RefreshToken newRefreshToken, User user);
    }
}