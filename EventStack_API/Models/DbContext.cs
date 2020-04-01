using EventStack_API.Helpers;
using Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Models
{
    public class DbContext : DbFactory<IOrganization>
    {
        public override void deleteMany(IEnumerable<IOrganization> delete)
        {
            throw new NotImplementedException();
        }

        public override void deleteOne(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void insertMany(IEnumerable<IOrganization> insert)
        {
            throw new NotImplementedException();
        }

        public override void insertOne(IOrganization insert)
        {
            throw new NotImplementedException();
        }

        public override void updateMany(IEnumerable<IOrganization> update)
        {
            throw new NotImplementedException();
        }

        public override void updateOne(IOrganization update)
        {
            throw new NotImplementedException();
        }
    }
}
