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
                    float timeSpanInSeconds = (float)Math.Round(timeSpan.TotalSeconds);
                    if (timeSpanInSeconds >= ship.TimeForAllShips)
                    {
                        ship.ClearConstructionStartDate();
                        for (var i = 0; i < ship.ShipsToBuild; i++)
                        {
                            user.BuildShip(ship.Type);
                        }
                        ship.IsShipUnderConstruction(false);
                        break;
                    }
                    else if (timeSpanInSeconds < ship.TimeForAllShips)
                    {
                        ship.SetTimeForAllShips(ship.TimeForAllShips - timeSpanInSeconds);
                        ship.SetConstructionStartDate(DateTime.UtcNow);
                        ship.IsShipUnderConstruction(true);
                        break;
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
            ship.SetShipsToBuild(CheckMaxShipCountThatPlayerCanAfford(user.Resources, ship.Cost, count));
            ship.SetTimeForAllShips(ship.TimeToBuildInSeconds * ship.ShipsToBuild);
            user.SpendResources(user.Resources, SetCostForManyShips(ship.Cost, ship.ShipsToBuild));
            ResetCost(ship.Cost, ship.ShipsToBuild);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }

        private List<Resource> SetCostForManyShips(List<Resource> shipCost, int shipCount)
        {
            foreach (var costResource in shipCost)
            {
                costResource.Value *= shipCount;
            }
            return shipCost;
        }

        private List<Resource> ResetCost(List<Resource> shipCost, int shipCount)
        {
            foreach (var costResource in shipCost)
            {
                costResource.Value /= shipCount;
            }
            return shipCost;
        }

        private int CheckMaxShipCountThatPlayerCanAfford(List<Resource> playerResources, List<Resource> shipCost, int shipCount)
        {
            foreach (var resource in shipCost)
            {
                var currentPlayerResource = playerResources.Where(c => c.Type == resource.Type).FirstOrDefault().Value;

                if (currentPlayerResource < resource.Value * shipCount)
                {
                    shipCount = (int)(currentPlayerResource / resource.Value);
                }
            }
            return shipCount;
        }
    }
}

