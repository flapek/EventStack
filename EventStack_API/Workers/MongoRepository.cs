using System.Linq;
using System.Collections.Generic;
using System;
using EventStack_API.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Threading;
using EventStack_API.Helpers;

namespace EventStack_API.Workers
{
    public class MongoRepository<T> : IRepositoryFactory<T> where T : IDbModel
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<T> Collection { get; set; }

        public MongoRepository(IDbContext context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<T>(typeof(T).Name);
        }

        #region Sync Method

        public bool Insert(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(T));

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                Collection.InsertOne(s, insert);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Insert(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                Collection.InsertMany(s, toInserts);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public T Find(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return Collection.Find(filter => filter.Id == id).Limit(1).FirstOrDefault();
        }

        public T Find(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();
 
            return Collection.Find(filter => filter.Id == toFind.Id).Limit(1).FirstOrDefault();
        }

        public IEnumerable<T> Find(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();

            var result = new List<T>();

            foreach (var toFind in toFinds)
                result.Add(Collection.Find(filter => filter.Id == toFind.Id).FirstOrDefault());

            return result;
        }

        public bool Update(T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                Collection.ReplaceOne(s, filter => filter.Id == toUpdate.Id, toUpdate);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Update(IEnumerable<T> toUpdates)
        {
            if (toUpdates == null)
                throw new ArgumentNullException();

           

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                foreach (var toUpdate in toUpdates)
                    Collection.ReplaceOne(session, filter => filter.Id == toUpdate.Id, toUpdate);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                Collection.DeleteOne(session, filter => filter.Id == id);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Delete(T toDelete)
        {
            if (toDelete == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                Collection.DeleteOne(session, filter => filter.Id == toDelete.Id);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Delete(IEnumerable<T> toDeletes)
        {
            if (toDeletes == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                foreach (var toDelete in toDeletes)
                    Collection.DeleteOne(session, filter => filter.Id == toDelete.Id);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        #endregion

        #region Async Method

        public async Task<bool> InsertAsync(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(T));

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.InsertOneAsync(session, insert);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> InsertAsync(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.InsertManyAsync(session, toInserts);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<T> FindAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
    
            var task = await Collection.FindAsync(filter => filter.Id == id);
            return await task.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var task = await Collection.FindAsync(filter => filter.Id == toFind.Id);
            return await task.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();
           
            var filter = Builders<T>.Filter.In(f => f.Id, toFinds.Select(toFind => toFind.Id));
            var task = await Collection.FindAsync(filter);
            return await task.ToListAsync();
        }

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.ReplaceOneAsync(session, filter => filter.Id == toUpdate.Id, toUpdate);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> toUpdates)
        {
            if (toUpdates == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                foreach (var toUpdate in toUpdates)
                    await Collection.ReplaceOneAsync(session, filter => filter.Id == toUpdate.Id, toUpdate);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.DeleteOneAsync(session, filter => filter.Id == id);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            if (toDelete == null)
                throw new ArgumentNullException();           

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.DeleteOneAsync(session, filter => filter.Id == toDelete.Id);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> toDeletes)
        {
            if (toDeletes == null)
                throw new ArgumentNullException();
           
            var filter = Builders<T>.Filter.In(f => f.Id, toDeletes.Select(d => d.Id));

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                await Collection.DeleteManyAsync(session, filter);
                return true;
            }, new TransactionOptions(), CancellationToken.None);
        }

        #endregion

    }
}