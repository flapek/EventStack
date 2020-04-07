using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IDbModel
    {
        ObjectId Id { get; set; }
        string Name { get; set; }
    }
}
