using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class CoverPhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }
        public int cover_id { get; set; }
        public float offset_x { get; set; }
        public float offset_y { get; set; }
        public string source { get; set; }
    }
}