using EventStack_API.Helpers;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace EventStack_API.Models
{
    public class DbContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => MongoDatabase.GetCollection<T>(name);
    }
}
