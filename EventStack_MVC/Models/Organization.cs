using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Geolocation; // https://github.com/scottschluer/geolocation
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_MVC.Interfaces;

namespace EventStack_MVC.Models
{
    public class Organization : IDbModel
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Name")]
        [BsonRequired]
        [Required(ErrorMessage = "Name must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")] 
        public string Name { get; set; }
        
        [BsonElement("Password")]
        [BsonRequired]
        [Required(ErrorMessage = "Password must be set!")]
        [StringLength(30, ErrorMessage = "The maximum number of character is 30!")]
        [RegularExpression("", ErrorMessage = "")] //TODO regex for Password and set ErrorMessage
        public string Password { get; set; }
        
        [BsonElement("Email")]
        [BsonRequired]
        [Required(ErrorMessage = "Email must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        [RegularExpression("", ErrorMessage = "")] //TODO regex for Email and set ErrorMessage
        public string Email { get; set; }
        
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        [BsonElement("Place")]  //TODO change place to Addres and create model for it 
        public Coordinate Place { get; set; }
        
        [BsonElement("Destription")] 
        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        public string Destription { get; set; }
        
        [BsonElement("Events")] 
        public IEnumerable<Event> Events { get; set; }
        
        [BsonElement("NIP")]
        [StringLength(10)] //TODO check lenght for NIP
        [RegularExpression("", ErrorMessage = "")] //TODO regex for NIP and set ErrorMessage
        public string NIP { get; set; }
        
        [BsonElement("REGON")]
        [StringLength(10)] //TODO check lenght for REGON
        [RegularExpression("", ErrorMessage = "")] //TODO regex for REGON and set ErrorMessage
        public string REGON { get; set; }
        
        [BsonElement("KRS")]
        [StringLength(10)] //TODO check lenght for KRS
        [RegularExpression("", ErrorMessage = "")] //TODO regex for KRS and set ErrorMessage
        public string KRS { get; set; }
    }
}