using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUserRepository _userRepository;

        public MessageService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteAsync(int id, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }            
            user.DeleteMessage(id);
            await _userRepository.UpdateAsync(user);
        }
    }
}