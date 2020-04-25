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
                try
                {
                    Collection.InsertOne(s, insert);
                    return true;
                }
                catch (Exception)
                {
                    s.AbortTransaction();
                    return false;
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Insert(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                try
                {
                    Collection.InsertMany(s, toInserts);
                    return true;
                }
                catch (Exception)
                {
                    s.AbortTransaction();
                    return false;
                }
            }, new TransactionOptions(), CancellationToken.None);
        }

        public T Find(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return Collection.Find(filter => filter.Id == id).FirstOrDefault();
        }

        public T Find(T toFind)         //TODO zastanowiæ siê czy potrzebne 
        {
            if (toFind == null)
                throw new ArgumentNullException();

            return Collection.Find(filter => filter.Id == toFind.Id).FirstOrDefault();
        }

        public T Update(string id, T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                try
                {
                    toUpdate.Id = id;
                    Collection.ReplaceOne(s, filter => filter.Id == id, toUpdate);
                    return toUpdate;
                }
                catch (Exception)
                {
                    s.AbortTransaction();
                    return Find(id);
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                try
                {
                    Collection.DeleteOne(session, filter => filter.Id == id);
                    return true;
                }
                catch (Exception)
                {
                    s.AbortTransaction();
                    return false;
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public bool Delete(T toDelete)  //TODO zastanowiæ siê czy potrzebne
        {
            if (toDelete == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return session.WithTransaction((s, c) =>
            {
                try
                {
                    Collection.DeleteOne(session, filter => filter.Id == toDelete.Id);
                    return true;
                }
                catch (Exception)
                {
                    s.AbortTransaction();
                    return false;
                }
               
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
                try
                {
                    await Collection.InsertOneAsync(session, insert);
                    return true;
                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> InsertAsync(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    await Collection.InsertManyAsync(session, toInserts);
                    return true;
                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<T> FindAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
    
            var task = await Collection.FindAsync(filter => filter.Id == id);
            return await task.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(T toFind)            //TODO zastanowiæ siê czy potrzebne
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var task = await Collection.FindAsync(filter => filter.Id == toFind.Id);
            return await task.FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(string id, T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    toUpdate.Id = id;
                    await Collection.ReplaceOneAsync(session, filter => filter.Id == toUpdate.Id, toUpdate);
                    return toUpdate;
                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return await FindAsync(id);
                }
                
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    await Collection.DeleteOneAsync(session, filter => filter.Id == id);
                    return true;
                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }
            }, new TransactionOptions(), CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(T toDelete)     //TODO zastanowiæ siê czy potrzebne
        {
            if (toDelete == null)
                throw new ArgumentNullException();           

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    await Collection.DeleteOneAsync(session, filter => filter.Id == toDelete.Id);
                    return true;
                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }
            }, new TransactionOptions(), CancellationToken.None);
        }

        #endregion

    }
}