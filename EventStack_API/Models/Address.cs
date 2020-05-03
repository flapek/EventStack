using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Address
    {
        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonElement("City")]
        public string City { get; set; }

        [BsonElement("Street")]
        [RegularExpression(@"(([a-zA-Z\p{L}])*\s*)*([0-9a-zA-Z./\-])*", ErrorMessage = "Street must contain name and number of street")]
        public string Street { get; set; }

        [BsonElement("ZipCode")]
        [RegularExpression(@"[0-9]{2}-[0-9]{3}")]
        public string ZipCode { get; set; }

        [BsonElement("Location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; private set; }

        public void SetLocation(double longitude, double latitude)
            => Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(longitude, latitude));
    }
}
