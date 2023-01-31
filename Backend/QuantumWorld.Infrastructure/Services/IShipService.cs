using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IShipService
    {
        Task BuildShip(ShipType type, string email);
    }
}