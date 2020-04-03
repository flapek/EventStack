using System.Collections.Generic;
using MongoDB.Bson;
using System;
using EventStack_API.Interfaces;

namespace EventStack_API.Models
{
    public class Repository<T> : IRepositoryFactory<T> where T: IDbModel
    {
        private DbContext Context { get; set; }
        private IDbModelValidator Validator { get; set; }
        public Repository(DbContext context, IDbModelValidator validator)
        {
            Context = context;
            Validator = validator;
        }

        public T Insert(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException();

            if (Validator.Validate(insert))
            {
                Context.GetCollection<T>(typeof(T).Name).InsertOne(insert);
                return insert;
            }

            return default;
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