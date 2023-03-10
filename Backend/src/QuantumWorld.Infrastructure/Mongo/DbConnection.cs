using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Mongo
{
    public interface IDbConnection
    {
        string DbName { get; }
        string UserCollectionName { get; }
        MongoClient Client { get; }
        IMongoCollection<User> UserCollection { get; }
    }

    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _db;
        private string _connectionId = "MongoDB";
        public string DbName { get; private set; }
        public string UserCollectionName { get; private set; } = "users";
        public MongoClient Client { get; private set; }
        public IMongoCollection<User> UserCollection { get; private set; }

        public DbConnection(IConfiguration config)
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString(_connectionId));
            DbName = _config["DatabaseName"];
            _db = Client.GetDatabase(DbName);

            UserCollection = _db.GetCollection<User>(UserCollectionName);
        }

    }
}