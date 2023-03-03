using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class ShipService : IShipService
    {
        private readonly IUserRepository _userRepository;

        public ShipService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task BuildShip(ShipType type, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var ship = user.Ships.Find(s => s.Type == type);
            for (var i = 0; i < (ship.ShipsToBuild); i++)
            {
                user.BuildShip(type);
                ship.ClearConstructionStartDate();
                ship.IsShipUnderConstruction(false);
                _userRepository.UpdateAsync(user);
            }
            await Task.CompletedTask;
        }
        public async Task CheckConstructionDates(User user)
        {
            DateTime now = DateTime.UtcNow;
            foreach (var ship in user.Ships)
            {
                if (ship.ConstructionStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - ship.ConstructionStartDate);
                    float timeSpanInSeconds = timeSpan.Seconds;
                    for (var i = 1; i < (ship.ShipsToBuild + 1); i++)
                    {
                        if (timeSpanInSeconds >= (ship.TimeToBuildInSeconds * i))
                        {
                            ship.ClearConstructionStartDate();
                            user.BuildShip(ship.Type);
                            ship.IsShipUnderConstruction(false);
                        }
                        else if (timeSpanInSeconds < (ship.TimeToBuildInSeconds * i))
                        {
                            ship.SetTimeToBuildInSeconds((ship.TimeToBuildInSeconds * i) - (timeSpanInSeconds));
                            ship.SetConstructionStartDate(DateTime.UtcNow);
                            ship.IsShipUnderConstruction(true);
                            break;
                        }
                    }
                }
            }
            await Task.CompletedTask;
        }

        public async Task SetConstructionStartDateAndShipCount(ShipType type, string username, DateTime date, int count)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var ship = user.Ships.Find(s => s.Type == type);
            ship.SetConstructionStartDate(date);
            ship.IsShipUnderConstruction(true);
            ship.SetShipsToBuild(count);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }
    }
}

