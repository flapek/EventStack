using EventStack_API.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventStack_API.Models
{
    public class MongoDbContext : IDbContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        public MongoClient MongoClient { get; private set; }

        public MongoDbContext(IOptions<DbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value._connectionString);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value._databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => MongoDatabase.GetCollection<T>(name);
    }
}
