namespace QuantumWorld.Infrastructure.Services
{
    public interface IEncrypter
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerivyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}