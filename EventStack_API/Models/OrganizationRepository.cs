using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;
using System;

namespace EventStack_API.Models
{
    public class OrganizationRepository : DbFactory<Organization>
    {
        private DbContext _context { get; set; }

        public OrganizationRepository(DbContext context) => _context = context;

        public override Organization insert(Organization insert)
        {
            if (insert == null)
                throw new ArgumentNullException();
            if (insert.Id == null)
                insert.Id = new ObjectId();

            if (ModelValid(insert))
            {
                _context.GetCollection<Organization>("Organization").InsertOne(insert);
                return insert;
            }

            return null;
        }

        public override IEnumerable<Organization> insert(IEnumerable<Organization> insert)
        {
            throw new NotImplementedException();
        }

        public override Organization find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override Organization find(Organization find)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Organization> find(IEnumerable<Organization> find)
        {
            throw new NotImplementedException();
        }

        public override Organization update(Organization update)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Organization> update(IEnumerable<Organization> update)
        {
            throw new NotImplementedException();
        }
        public override bool delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override bool delete(Organization delete)
        {
            throw new NotImplementedException();
        }

        public override bool delete(IEnumerable<Organization> delete)
        {
            throw new NotImplementedException();
        }

        private bool ModelValid(Organization insert) => insert.Name != null && insert.Password != null && insert.Email != null;
    }
}