using System.Text;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Repositories
{
    public class InMemoryUserRopository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>{
            new User("email@email", "password123", Encoding.ASCII.GetBytes("12345"), Encoding.ASCII.GetBytes("12345"), "Kamil"),
            new User("email1@next", "password123", Encoding.ASCII.GetBytes("54321"), Encoding.ASCII.GetBytes("54321"), "Bartek")
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