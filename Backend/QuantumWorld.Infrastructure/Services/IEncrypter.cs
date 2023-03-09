using System.Security;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IEncrypter
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}