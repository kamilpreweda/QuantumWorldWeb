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
            user.BuildShip(type);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }
        public async Task CheckConstructionDates(User user)
        {
            DateTime now = DateTime.UtcNow;
            // int shipsAlreadyBuilt = 0;
            foreach (var ship in user.Ships)
            {
                if (ship.ConstructionStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - ship.ConstructionStartDate);
                    float timeSpanInSeconds = timeSpan.Seconds;

                    while (timeSpanInSeconds >= ship.TimeToBuildInSeconds && ship.ShipsAlreadyBuilt < ship.ShipsToBuild)
                    {
                        ship.IncreaseShipsAlreadyBuiltByOne();
                        timeSpanInSeconds -= ship.TimeToBuildInSeconds;
                    }
                    if (ship.ShipsAlreadyBuilt > 0)
                    {
                        for (var i = 0; i < ship.ShipsAlreadyBuilt; i++)
                        {
                            user.BuildShip(ship.Type);
                        }
                    }
                    if (ship.ShipsAlreadyBuilt < ship.ShipsToBuild)
                    {
                        ship.SetTimeToBuildInSeconds(ship.TimeToBuildInSeconds - timeSpanInSeconds);
                        ship.SetConstructionStartDate(DateTime.UtcNow);
                        ship.IsShipUnderConstruction(true);
                        break;
                    }
                    else
                    {
                        ship.ClearShipsAlreadyBuilt();
                        ship.ClearConstructionStartDate();
                        ship.IsShipUnderConstruction(false);
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

