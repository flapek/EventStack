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

        public override Category insert(Category insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Category> insert(IEnumerable<Category> insert)
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

        public override IEnumerable<Category> find(IEnumerable<Category> find)
        {
            throw new System.NotImplementedException();
        }

        public override Category update(Category update)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Category> update(IEnumerable<Category> update)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(Category delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(IEnumerable<Category> delete)
        {
            throw new System.NotImplementedException();
        }
    }
}