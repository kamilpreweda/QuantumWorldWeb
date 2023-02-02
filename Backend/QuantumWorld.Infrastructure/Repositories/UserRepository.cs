using MongoDB.Driver;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Mongo;

namespace QuantumWorld.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IDbConnection db)
        {
            _users = db.UserCollection;
        }
        public async Task<User> GetAsync(Guid id)
            => _users.AsQueryable().FirstOrDefault(x => x.Id == id);


        public User Get(string email)
        {
            var user = _users.AsQueryable().FirstOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<IEnumerable<User>> BrowseAsync()
            => await _users.AsQueryable().ToListAsync();


        public async Task AddAsync(User user)
            => await _users.InsertOneAsync(user);

        public async Task RemoveAsync(Guid id)
            => await _users.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(User user)
            => await _users.ReplaceOneAsync(x => x.Id == user.Id, user);

    }
}