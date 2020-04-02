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
        public IMongoDatabase MongoDatabase { get; private set; }
        private MongoClient MongoClient { get; set; }
        private IMongoCollection<Organization> MongoCollection { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
            MongoCollection = MongoDatabase.GetCollection<Organization>("Organization");
        }

        public override Organization insertOne(Organization insert)
        {
            if (insert == null)
                throw new ArgumentNullException();

            if (insert.Id == null)
                insert.Id = new ObjectId();

            if (validModel(insert))
            {
                //var collection = MongoDatabase.GetCollection<Organization>("Organizaction");
                //collection.InsertOne(insert);
                return insert;
            }

            return null;
        }

        public override IEnumerable<Organization> insertMany(IEnumerable<Organization> insert)
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

        public override IEnumerable<Organization> findMany(IEnumerable<Organization> find)
        {
            throw new NotImplementedException();
        }

        public override Organization updateOne(Organization update)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Organization> updateMany(IEnumerable<Organization> update)
        {
            throw new NotImplementedException();
        }

        public override bool deleteOne(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override bool deleteOne(Organization delete)
        {
            throw new NotImplementedException();
        }

        public override bool deleteMany(IEnumerable<Organization> delete)
        {
            throw new NotImplementedException();
        }

        private bool validModel(Organization insert) => insert.Name != null && insert.Password != null && insert.Email != null;
    }
}
