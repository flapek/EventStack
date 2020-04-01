using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Interfaces
{
    public interface ICategory 
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}