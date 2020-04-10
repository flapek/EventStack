using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using EventStack_API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Organization : IDbModel
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        [Required(ErrorMessage = "Id must be defined!")]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        [Required(ErrorMessage = "Name must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        public string Name { get; set; }

        [BsonElement("Password")]
        [BsonRequired]
        [Required(ErrorMessage = "Password must be set!")]
        [StringLength(30, ErrorMessage = "The maximum number of character is 30!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain minimum eight characters, at least one letter and one number")]
        public string Password
        {
            get => Password;
            set => Password = PasswordHasher.ComputeHash(value);
        }

        [BsonElement("Email")]
        [BsonRequired]
        [Required(ErrorMessage = "Email must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email must contain dot, @ and only lowercase letters")]
        public string Email { get; set; }

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("Address")]
        public Address Address { get; set; }

        [BsonElement("Destription")]
        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        public string Destription { get; set; }

        [BsonElement("Events")]
        public IEnumerable<Event> Events { get; set; }

        [BsonElement("NIP")]
        [StringLength(10)]
        [RegularExpression("", ErrorMessage = "")] //TODO regex for NIP and set ErrorMessage
        public string NIP { get; set; }

        [BsonElement("REGON")]
        [StringLength(9)]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "REGON must contain 9 digit")] //TODO regex for REGON and set ErrorMessage
        public string REGON { get; set; }
    }
}