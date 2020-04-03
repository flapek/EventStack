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

        public T Insert(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(T));

            collection.InsertOne(insert);
            return insert;
        }

        public IEnumerable<T> Insert(IEnumerable<T> insert)
        {
            if (insert == null)
                throw new ArgumentNullException();
            return null;
        }

        public T Find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public T Find(T find)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(IEnumerable<T> find)
        {
            throw new NotImplementedException();
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