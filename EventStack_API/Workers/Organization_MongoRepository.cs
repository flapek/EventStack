using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Workers
{
    public class Organization_MongoRepository : MongoRepository<Organization>, IRepositoryFactory<Organization>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Organization> Collection { get; set; }

        public Organization_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<Organization>(typeof(Organization).Name);
        }

        public OrganizationWithoutImportantData Find(Filter filter)
            => Collection.Find(x => x.Name == filter.Name && x.Email == filter.Email).FirstOrDefault();

        public class Filter
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
            [RegularExpression(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*",
                ErrorMessage = "Email must contain eg. example@example.com")]
            public string Email { get; set; }
        }
    }
}
