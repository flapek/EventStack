using EventStack_API.Helpers;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace EventStack_API.Models
{
    public class DbContext : DbFactory<Organization>
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public override Organization insertOne(Organization insert)
        {
            if (insert == null)
                throw new ArgumentNullException();
            if (insert.Id == null)
                insert.Id = new ObjectId();
            return insert;
        }

        public override void insertMany(IEnumerable<Organization> insert)
        {
            throw new NotImplementedException();
        }

        public override void find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void find(Organization find)
        {
            throw new NotImplementedException();
        }

        public override void findMany(IEnumerable<Organization> find)
        {
            throw new NotImplementedException();
        }

        public override void updateOne(Organization update)
        {
            throw new NotImplementedException();
        }

        public override void updateMany(IEnumerable<Organization> update)
        {
            throw new NotImplementedException();
        }

        public override void deleteOne(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override void deleteOne(Organization delete)
        {
            throw new NotImplementedException();
        }

        public override void deleteMany(IEnumerable<Organization> delete)
        {
            throw new NotImplementedException();
        }
    }
}
