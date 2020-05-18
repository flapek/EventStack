using MongoDB.Driver.GeoJsonObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EventStack_MVC.Models
{
    public class Address
    {
        public string Country { get; set; }

        public string City { get; set; }

        [RegularExpression(@"(([a-zA-Z\p{L}])*\s*-*)*([0-9a-zA-Z./\-])*", ErrorMessage = "Street must contain name and number of street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "ZipCode must be set!")]
        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "ZipCode must contain eg. 23-345")]
        public string ZipCode { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}