using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Place
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int id { get; set; }
        public int location { get; set; }
        public int name { get; set; }
        public int overall_rating { get; set; }
    }
}