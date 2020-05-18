using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Workers
{
    public class Category_MongoRepository : MongoRepository<Category>, IRepositoryFactory<Category>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Category> Collection { get; set; }

        public Category_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<Category>(typeof(Category).Name);
        }

        public IEnumerable<Category> Find() => Collection.Find(x => true).ToList();

        public async Task<IEnumerable<Category>> FindAsync()
        {
            var result = await Collection.FindAsync(x => true);
            return await result.ToListAsync();
        }
    }
}
