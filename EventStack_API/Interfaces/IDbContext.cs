using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.Interfaces
{
    public interface IDbContext
    {
        MongoClient MongoClient { get; set; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}