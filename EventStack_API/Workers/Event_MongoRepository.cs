using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Workers
{
    public class Event_MongoRepository : MongoRepository<Event>, IRepositoryFactory<Event>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Event> Collection { get; set; }

        public Event_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<Event>(typeof(Event).Name);
        }

        public IEnumerable<Event> Find() => Collection.Find(x => true).ToList();

        public IEnumerable<Event> Find(Filter filter)
        {
            var locationQuery = new FilterDefinitionBuilder<Event>()
                .Near(e => e.Place.Location, filter.Coordinates.Latitude, filter.Coordinates.Longitude, filter.MaxDistance);

            return Collection.Find(locationQuery).ToList();
        }

        public async Task<IEnumerable<Event>> FindAsync()
        {
            var result = await Collection.FindAsync(x => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Event>> FindAsync(Filter filter)
        {
            var locationQuery = new FilterDefinitionBuilder<Event>()
                .Near(e => e.Place.Location, filter.Coordinates.Latitude, filter.Coordinates.Longitude, maxDistance: filter.MaxDistance);
            var result = await Collection.FindAsync(locationQuery);
            return await result.ToListAsync();
        }

        public class Filter
        {
            public GeoJson2DGeographicCoordinates Coordinates { get; set; }
            public double? MaxDistance { get; set; }
        }
    }
}
