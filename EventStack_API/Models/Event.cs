using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        [Required(ErrorMessage = "Name must be defined!")]
        [StringLength(50, ErrorMessage = "The maximum number of character is 50!")]
        public string Name { get; set; }

        [BsonElement("Categories")]
        public IEnumerable<string> CategoriesId { get; set; }

        [BsonElement("Photo")]
        [BsonRequired]
        [Required(ErrorMessage = "Photo must be added for event!")]
        public byte[] Photo { get; set; }

        [BsonElement("Description")]
        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        [BsonRequired]
        [Required(ErrorMessage = "Event must have short description!")]
        public string Description { get; set; }

        [BsonElement("StartTime")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonRequired]
        [Required(ErrorMessage = "Event start date must be defined!")]
        public DateTime StartTime { get; set; }

        [BsonElement("EndTime")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonRequired]
        [Required(ErrorMessage = "Event end date must be defined!")]
        public DateTime EndTime { get; set; }

        [BsonElement("PublishTime")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonRequired]
        [Required(ErrorMessage = "Publish date must be set")]
        public DateTime PublishTime { get; set; }

        [BsonElement("Place")]
        [BsonRequired]
        [Required(ErrorMessage = "Place for event must be set")]
        public Address Place { get; set; }

        [BsonElement("IsCanceled")]
        [BsonDefaultValue(false)]
        [BsonRequired]
        [Required(ErrorMessage = "Flag must be set!")]
        public bool IsCanceled { get; set; }

        [BsonElement("FacebookURL")]
        public string FacebookURL { get; set; }

        [BsonElement("WebSiteURL")]
        public string WebSiteURL { get; set; }
    }
}