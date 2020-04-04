using System.Collections.Generic;
using MongoDB.Bson;
using System;
using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.Models
{
    public class Repository<T> : IRepositoryFactory<T> where T : IDbModel
    {
        private IDbContext _context { get; set; }

        public Repository(IDbContext context)
        {
            _context = context;
        }

        public T Insert(T toInsert)
        {
            if (toInsert == null)
                throw new ArgumentNullException(nameof(T));

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.InsertOne(session, toInsert);
                    session.CommitTransaction();
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                }
            }
            return toInsert;
        }

        public IEnumerable<T> Insert(IEnumerable<T> toInsert)
        {
            if (toInsert == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.InsertMany(session, toInsert);
                    session.CommitTransaction();
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                }
            }
            return toInsert;
        }

        public T Find(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        public T Find(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", toFind.Id);
            return collection.Find(filter).First();
        }

        public IEnumerable<T> Find(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);
            var filters = new List<FilterDefinition<T>>();
            
            foreach (var toFind in toFinds)
                filters.Add(Builders<T>.Filter.Eq("Id", toFind.Id));
            
            var result = new List<T>();

            foreach (var filter in filters)
                result.Add(collection.Find(filter).First());

            return result;
        }

        public T Update(T update)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Update(IEnumerable<T> update)
        {
            throw new NotImplementedException();
        }
        public bool Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T delete)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IEnumerable<T> delete)
        {
            throw new NotImplementedException();
        }
    }
}