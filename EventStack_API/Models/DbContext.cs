using EventStack_API.Helpers;
using Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace EventStack_API.Models
{
    public class DbContext : DbFactory<IOrganization>
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public override void insertOne(IOrganization insert)
        {
            throw new NotImplementedException();
        }

        public override void insertMany(IEnumerable<IOrganization> insert)
        {
            throw new NotImplementedException();
        }

        public override void find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void find(IOrganization find)
        {
            throw new NotImplementedException();
        }

        public override void findMany(IEnumerable<IOrganization> find)
        {
            throw new NotImplementedException();
        }

        public override void updateOne(IOrganization update)
        {
            throw new NotImplementedException();
        }

        public override void updateMany(IEnumerable<IOrganization> update)
        {
            throw new NotImplementedException();
        }

        public override void deleteOne(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void deleteOne(IOrganization delete)
        {
            throw new NotImplementedException();
        }

        public override void deleteMany(IEnumerable<IOrganization> delete)
        {
            throw new NotImplementedException();
        }
    }
}
