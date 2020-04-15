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
    public class Event_MongoRepository : MongoRepository<Event>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Event> Collection { get; set; }

        public Event_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<Event>(typeof(Event).Name);
        }

        public IEnumerable<Event> Find(GeoJson2DGeographicCoordinates coordinates, double distance)
        {
            var locationQuery = new FilterDefinitionBuilder<Event>().Near(e => e.Place.Location, coordinates.Latitude, coordinates.Longitude, distance);
            return Collection.Find(locationQuery).ToList();
        }
    }
}
