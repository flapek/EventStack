﻿using EventStack_API.Helpers;
using Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace EventStack_API.Models
{
    public class DbContext : DbFactory<IOrganization>
    {
        public IMongoDatabase mongoDatabase { get; set; }

        public DbContext(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

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

        public override void findMany(IEnumerable<IOrganization> find)
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
