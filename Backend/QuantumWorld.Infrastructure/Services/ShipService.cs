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
        public async Task BuildShip(ShipType type, string email)
        {
            var user = _userRepository.Get(email);
           if (user is null)
            {
                throw new Exception($"User with {email} doesn't exist!");
            }
            user.BuildShip(type);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }
    }
}

