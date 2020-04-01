using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Interface
{
    public interface ICategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }
        public string Name { get; set; }
    }
}