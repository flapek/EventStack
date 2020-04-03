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
        private IMongoDatabase _mongoDatabase { get; set; }
        private MongoClient _mongoClient { get; set; }

        public DbContext(IOptions<DbSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value._connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(configuration.Value._databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _mongoDatabase.GetCollection<T>(name);
    }
}
