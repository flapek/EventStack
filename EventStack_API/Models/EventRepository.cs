using System.Collections.Generic;
using EventStack_API.Helpers;
using Models;
using MongoDB.Bson;

namespace EventStack_API.Models
{
    public class EventRepository : DbFactory<Event>
    {
        private DbContext _context { get; set; }

        public EventRepository(DbContext context) => _context = context;

        public override Event insert(Event insert)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Event> insert(IEnumerable<Event> insert)
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

        public override IEnumerable<Event> find(IEnumerable<Event> find)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Event> update(IEnumerable<Event> update)
        {
            throw new System.NotImplementedException();
        }

        public override Event update(Event update)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(Event delete)
        {
            throw new System.NotImplementedException();
        }

        public override bool delete(IEnumerable<Event> delete)
        {
            throw new System.NotImplementedException();
        }
    }
}