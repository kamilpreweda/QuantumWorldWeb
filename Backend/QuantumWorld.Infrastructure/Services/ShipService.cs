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
        public async Task BuildShip(ShipType type, int count, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            user.BuildShip(type, count);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }
    }
}

