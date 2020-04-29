﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Models
{
    public class OrganizationWithoutImportantData
    { 
        [BsonElement("Name")]
        [BsonRequired]
        [Required(ErrorMessage = "Name must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        public string Name { get; set; }

        [BsonElement("Email")]
        [BsonRequired]
        [Required(ErrorMessage = "Email must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        [RegularExpression(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*", ErrorMessage =
            "Email must contain eg. example@example.com")]
        public string Email { get; set; }

        [BsonElement("PhoneNumber")]
        [RegularExpression(@"((?(\+[0-9]{2})\+[0-9]{2}\s[0-9]{9}|\s[0-9]{9})", ErrorMessage = "Number must contain +48 999999999 or 999999999")]
        public string PhoneNumber { get; set; }

        [BsonElement("Address")]
        public Address Address { get; set; }

        [BsonElement("Description")]
        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        public string Description { get; set; }

        [BsonElement("Events")]
        public IEnumerable<string> EventsId { get; set; }

        [BsonElement("NIP")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "NIP must contain 10 digit")]
        public string NIP { get; set; }

        [BsonElement("REGON")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "REGON must contain 9 digit")]
        public string REGON { get; set; }
    }
}
