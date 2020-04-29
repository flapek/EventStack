using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using EventStack_API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.Models
{
    public class Organization : OrganizationWithoutImportantData, IDbModel
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Password")]
        [BsonRequired]
        [Required(ErrorMessage = "Password must be set!")]
        [StringLength(30, ErrorMessage = "The maximum number of character is 30!")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-+,\(\)]).{8,}$", ErrorMessage =
            "Password must contain at least 1 lowercase and uppercase alphabetical character, 1 numeric character, 1 special character(!,@,#,$,%,^,&,*) and must be eight characters or longer!")]
        public string Password { get; set; }
    }
}