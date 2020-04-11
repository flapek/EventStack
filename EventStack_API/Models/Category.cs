using EventStack_API.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Category : IDbModel
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        [Required(ErrorMessage = "Id must be defined!")]
        public string Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        [Required(ErrorMessage = "Name must be defined!")]
        [StringLength(50, ErrorMessage = "The maximum number of character is 50!")]
        public string Name { get; set; }
    } 
}