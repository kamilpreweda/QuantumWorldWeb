using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Repositories
{
    public class InMemoryUserRopository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>{
            new User("email@email", "password123", "12345", "Kamil"),
            new User("email1@next", "password123", "12345", "Bartek")
        };
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User Get(Guid id)
        {
            return _users.SingleOrDefault(x => x.Id == id);
        }

        public User Get(string email)
        {
            return _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}