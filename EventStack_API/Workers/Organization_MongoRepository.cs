using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
    }
}
