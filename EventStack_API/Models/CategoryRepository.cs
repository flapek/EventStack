using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;

namespace EventStack_API.Models
{
    public class CategoryRepository : DbFactory<Category>
    {
        private DbContext _context { get; set; }
        
        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public override bool deleteMany(IEnumerable<Category> delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(Category delete)
        {
            throw new System.NotImplementedException();
        }

        public override Category find(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override Category find(Category find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Category> findMany(IEnumerable<Category> find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Category> insertMany(IEnumerable<Category> insert)
        {
            throw new System.NotImplementedException();
        }

        public override Category insertOne(Category insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Category> updateMany(IEnumerable<Category> update)
        {
            throw new System.NotImplementedException();
        }

        public override Category updateOne(Category update)
        {
            throw new System.NotImplementedException();
        }
    }
}