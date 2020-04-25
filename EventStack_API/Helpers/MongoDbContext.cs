using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.Helpers
{
    public class MongoDbContext : IDbContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        public MongoClient MongoClient { get; private set; }

        public MongoDbContext(IDbSettings configuration)
        {
            MongoClient = new MongoClient(configuration.ConnectionString);
            MongoDatabase = MongoClient.GetDatabase(configuration.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => MongoDatabase.GetCollection<T>(name);
    }
}