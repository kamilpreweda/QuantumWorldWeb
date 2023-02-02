using QuantumWorld.Core.Domain;

namespace QuantumWorld.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        User Get(string email);
        Task<IEnumerable<User>> BrowseAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid id);
    }
}