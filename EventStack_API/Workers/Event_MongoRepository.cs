using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventStack_API.Workers
{
    public class Event_MongoRepository : MongoRepository<Event>, IRepositoryFactory<Event>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Event> CollectionEvent { get; set; }
        private IMongoCollection<Organization> CollectionOrganization { get; set; }

        public Event_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            CollectionEvent = Context.GetCollection<Event>(typeof(Event).Name);
            CollectionOrganization = Context.GetCollection<Organization>(typeof(Organization).Name);
        }

        public IEnumerable<Event> Find() => CollectionEvent.Find(x => true).ToList();

        public IEnumerable<Event> Find(Filter filter)
        {
            var locationQuery = new FilterDefinitionBuilder<Event>()
                .Near(e => e.Place.Location, filter.Coordinates.Latitude, filter.Coordinates.Longitude, filter.MaxDistance);

            return CollectionEvent.Find(locationQuery).ToList();
        }

        public async Task<IEnumerable<Event>> FindAsync()
        {
            var result = await CollectionEvent.FindAsync(x => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Event>> FindAsync(Filter filter)
        {
            var locationQuery = new FilterDefinitionBuilder<Event>()
                .Near(e => e.Place.Location, filter.Coordinates.Latitude, filter.Coordinates.Longitude, maxDistance: filter.MaxDistance);
            var result = await CollectionEvent.FindAsync(locationQuery);
            return await result.ToListAsync();
        }

        public async Task<bool> InsertAsync(string secret, Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(Organization));

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    var organization = await CollectionOrganization.FindAsync(secret).Result.FirstOrDefaultAsync();
                    if (organization != null)
                    {
                        await CollectionEvent.InsertOneAsync(s, @event);
                        organization.EventsId.Append(@event.Id);
                        await CollectionOrganization.ReplaceOneAsync(s, x => x.Id == organization.Id, organization);
                        return true;
                    }
                    else
                    {
                        await s.AbortTransactionAsync();
                        return false;
                    }

                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }

            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<Event> UpdateAsync(string id, string secret, Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(Organization));

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    var organization = await CollectionOrganization.FindAsync(secret).Result.FirstOrDefaultAsync();
                    if (organization != null)
                    {
                        await CollectionEvent.ReplaceOneAsync(s, x => x.Id == id, @event);
                        return @event;
                    }
                    else
                    {
                        await s.AbortTransactionAsync();
                        return null;
                    }

                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return null;
                }

            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> Delete(string id, string secret)
        {
            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    var organization = await CollectionOrganization.FindAsync(secret).Result.FirstOrDefaultAsync();
                    if (organization != null)
                    {
                        organization.EventsId.TakeWhile(x => x == id);
                        await CollectionEvent.DeleteOneAsync(x => x.Id == id);
                        await CollectionOrganization.ReplaceOneAsync(s, x => x.Id == organization.Id, organization);
                        return true;
                    }
                    else
                    {
                        await s.AbortTransactionAsync();
                        return false;
                    }

                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }

            }, new TransactionOptions(), CancellationToken.None);
        }

        public class Filter
        {
            public GeoJson2DGeographicCoordinates Coordinates { get; set; }
            public double? MaxDistance { get; set; }
        }
    }
}
