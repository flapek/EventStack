using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;

namespace EventStack_API.Models
{
    public class OrganizationRepository : DbFactory<Organization>
    {
        private DbContext _context { get; set; }
        public OrganizationRepository(DbContext context)
        {
            _context = context;
        }

        public override bool deleteMany(IEnumerable<Organization> delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(Organization delete)
        {
            throw new System.NotImplementedException();
        }

        public override Organization find(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override Organization find(Organization find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Organization> findMany(IEnumerable<Organization> find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Organization> insertMany(IEnumerable<Organization> insert)
        {
            throw new System.NotImplementedException();
        }

        public override Organization insertOne(Organization insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Organization> updateMany(IEnumerable<Organization> update)
        {
            throw new System.NotImplementedException();
        }

        public override Organization updateOne(Organization update)
        {
            throw new System.NotImplementedException();
        }
    }
}