using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventStack_API.Models
{
    public class DbContext
    {
        private IMongoDatabase _mongoDatabase { get; set; }
        private MongoClient _mongoClient { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value._connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(configuration.Value._databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _mongoDatabase.GetCollection<T>(name);
    }
}
