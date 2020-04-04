using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}