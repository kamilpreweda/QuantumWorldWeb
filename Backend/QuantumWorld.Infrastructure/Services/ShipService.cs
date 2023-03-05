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
            foreach (var ship in user.Ships)
            {
                if (ship.ConstructionStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - ship.ConstructionStartDate);
                    int timeSpanInSeconds = (int)timeSpan.TotalSeconds;
                    int totalTimeSpan = 0;
                    totalTimeSpan += timeSpanInSeconds;

                    while (totalTimeSpan >= ship.TimeToBuildInSeconds && ship.ShipsAlreadyBuilt < ship.ShipsToBuild)
                    {
                        ship.IncreaseShipsAlreadyBuiltByOne();
                        totalTimeSpan -= ship.TimeToBuildInSeconds;
                    }
                    if (ship.ShipsAlreadyBuilt >= ship.ShipsToBuild)
                    {
                        int totalShips = ship.ShipsAlreadyBuilt + ship.Count;
                        if (totalShips < ship.ShipsToBuild)
                        {
                            for (var i = 0; i < ship.ShipsAlreadyBuilt; i++)
                            {
                                user.BuildShip(ship.Type);
                                ship.DecreaseShipsToBuidByOne();
                            }
                        }
                    }
                    if (ship.ShipsAlreadyBuilt < ship.ShipsToBuild)
                    {
                        ship.SetTimeToBuildInSeconds(ship.TimeToBuildInSeconds - totalTimeSpan);
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

