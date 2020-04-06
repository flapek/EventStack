using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Geolocation; // https://github.com/scottschluer/geolocation
using System.Collections.Generic;

namespace Models
{
    public class Organization
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("Place")] 
        public Coordinate Place { get; set; }
        [BsonElement("Destription")] 
        public string Destription { get; set; }
        [BsonElement("Events")] 
        public IEnumerable<Event> Events { get; set; }
        [BsonElement("NIP")]
        public string NIP { get; set; }
        [BsonElement("REGON")]
        public string REGON { get; set; }
        [BsonElement("KRS")]
        public string KRS { get; set; }
    }
}