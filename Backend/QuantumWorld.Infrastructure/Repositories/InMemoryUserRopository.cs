using System.Text;
using MongoDB.Bson;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Repositories
{
    public class InMemoryUserRopository : IUserRepository
    {
        private static readonly IBattle _battle;
        private static ISet<User> _users = new HashSet<User>{
              new User(Guid.NewGuid(), Encoding.ASCII.GetBytes("12345"), Encoding.ASCII.GetBytes("12345"), "Kamil")
        };
        async Task IUserRepository.AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return _users.SingleOrDefault(x => x.Id == id);

        }
         public User GetByUsername(string username)
        {
            return _users.SingleOrDefault(x => x.Username == username.ToLowerInvariant());

        }

        async Task<IEnumerable<User>> IUserRepository.BrowseAsync()
        {
            return _users.ToList();
        }

        async Task IUserRepository.RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _users.Remove(user);
        }

        public void UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}