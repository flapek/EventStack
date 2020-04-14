using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Geolocation;
using EventStack_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Event : IDbModel
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        [Required(ErrorMessage ="Name must be defined!")]
        [StringLength(50, ErrorMessage = "The maximum number of character is 50!")]
        public string Name { get; set; }

        [BsonElement("Categories")]
        public IEnumerable<Category> Categories { get; set; }

        [BsonElement("Photo")]
        [BsonRequired]
        [Required(ErrorMessage = "Photo must be added for event!")]
        public string Photo { get; set; }

        [BsonElement("Description")]
        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        [BsonRequired]
        [Required(ErrorMessage = "Event must have short description!")]
        public string Description { get; set; }

        [BsonElement("StarTime")]
        [BsonRequired]
        [Required(ErrorMessage = "Event start date must be defined!")]
        public DateTime StarTime { get; set; }

        [BsonElement("EndTime")]
        [BsonRequired]
        [Required(ErrorMessage = "Event end date must be defined!")]
        public DateTime EndTime { get; set; }

        [BsonElement("PublishTime")]
        [BsonRequired]
        [Required(ErrorMessage = "Publish datemust be set")]
        public DateTime PublishTime { get; set; }

        [BsonElement("Place")]
        [BsonRequired]
        [Required(ErrorMessage ="Place for event must be set")]
        public Address Place { get; set; }
        
        [BsonElement("IsCanceled")]
        [BsonDefaultValue(false)]
        [BsonRequired]
        [Required(ErrorMessage = "Flag must be set!")]
        public bool IsCanceled { get; set; }
        
        [BsonElement("FacebookURL")]
        public Uri FacebookURL { get; set; }
        
        [BsonElement("WebSiteURL")]
        public Uri WebSiteURL { get; set; }
    }
}