using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;

namespace EventStack_API.Models
{
    public class OrganizationRepository : DbFactory<Organization>
    {
        private DbContext _context { get; set; }

        public override bool delete(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(Organization delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(IEnumerable<Organization> delete)
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

        public override IEnumerable<Organization> find(IEnumerable<Organization> find)
        {
            throw new System.NotImplementedException();
        }

        public override Organization insert(Organization insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Organization> insert(IEnumerable<Organization> insert)
        {
            throw new System.NotImplementedException();
        }

        public override Organization update(Organization update)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Organization> update(IEnumerable<Organization> update)
        {
            throw new System.NotImplementedException();
        }
    }
}