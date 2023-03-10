using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IShipService
    {
        Task BuildShip(ShipType type, string email);
        Task SetConstructionStartDateAndShipCount(ShipType type, string username, DateTime date, int count);
        Task CheckConstructionDates(User user);
    }
}