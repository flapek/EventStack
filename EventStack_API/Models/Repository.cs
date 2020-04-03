using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;
using System;
using EventStack_API.Interfaces;

namespace EventStack_API.Models
{
    public class Repository<T> : DbFactory<T> where T: IBaseDbModel
    {
        private DbContext _context { get; set; }
        private IDbModelValidator _validator { get; set; }
        public Repository(DbContext context, IDbModelValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public override T insert(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException();
            if (insert.Id == null)
                insert.Id = new ObjectId();

            if (_validator.Validate(insert))
            {
                _context.GetCollection<T>(typeof(T).Name).InsertOne(insert);
                return insert;
            }

            return default;
        }

        public override IEnumerable<T> insert(IEnumerable<T> insert)
        {
            if (insert == null)
                throw new ArgumentNullException();
            return null;
        }

        public override T find(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override T find(T find)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> find(IEnumerable<T> find)
        {
            throw new NotImplementedException();
        }

        public override T update(T update)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> update(IEnumerable<T> update)
        {
            throw new NotImplementedException();
        }
        public override bool delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public override bool delete(T delete)
        {
            throw new NotImplementedException();
        }

        public override bool delete(IEnumerable<T> delete)
        {
            throw new NotImplementedException();
        }
    }
}