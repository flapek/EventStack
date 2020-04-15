using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Address
    {
        //[BsonId]
        //[BsonElement("Id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public ObjectId Id { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonElement("City")]
        public string City { get; set; }

        [BsonElement("Street")]
        public string Street { get; set; }

        [BsonElement("ZipCode")]
        public string ZipCode { get; set; }

        [BsonElement("Location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}