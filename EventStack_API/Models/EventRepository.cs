using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;

namespace EventStack_API.Models
{
    public class EventRepository : DbFactory<Event>
    {
        private DbContext _context { get; set; }

        public EventRepository(DbContext context)
        {
            _context = context;
        }

        public override bool deleteMany(IEnumerable<Event> delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool deleteOne(Event delete)
        {
            throw new System.NotImplementedException();
        }

        public override Event find(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override Event find(Event find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Event> findMany(IEnumerable<Event> find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Event> insertMany(IEnumerable<Event> insert)
        {
            throw new System.NotImplementedException();
        }

        public override Event insertOne(Event insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Event> updateMany(IEnumerable<Event> update)
        {
            throw new System.NotImplementedException();
        }

        public override Event updateOne(Event update)
        {
            throw new System.NotImplementedException();
        }
    }
}