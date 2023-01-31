namespace QuantumWorld.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}