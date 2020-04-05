using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using System;
using EventStack_API.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace EventStack_API.Models
{
    public class MongoRepository<T> : IRepositoryFactory<T> where T : IDbModel
    {
        private MongoDbContext Context { get; set; }

        public MongoRepository(MongoDbContext context)
        {
            Context = context;
        }

        #region Sync Method

        public bool Insert(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(T));

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.InsertOne(session, insert);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public bool Insert(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.InsertMany(session, toInserts);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public T Find(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            return collection.Find(filter => filter.Id == id).Limit(1).FirstOrDefault();
        }

        public T Find(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            return collection.Find(filter => filter.Id == toFind.Id).Limit(1).FirstOrDefault();
        }

        public IEnumerable<T> Find(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            var filters = new List<Func<T, bool>>();

            foreach (var toFind in toFinds)
                filters.Add(filter => filter.Id == toFind.Id);

            var result = new List<T>();

            foreach (var filter in filters)
                result.Add(collection.Find(finded => filter.Equals(finded)).Limit(1).FirstOrDefault());

            return result;
        }

        public bool Update(T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.ReplaceOne(session, filter => filter.Id == toUpdate.Id, toUpdate);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public bool Update(IEnumerable<T> toUpdates)
        {
            if (toUpdates == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    foreach(var toUpdate in toUpdates)
                        collection.ReplaceOne(session, filter => filter.Id == toUpdate.Id, toUpdate);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }
        
        public bool Delete(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.DeleteOne(session, filter => filter.Id == id);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public bool Delete(T toDelete)
        {
            if (toDelete == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.DeleteOne(session, filter => filter.Id == toDelete.Id);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public bool Delete(IEnumerable<T> toDeletes)
        {
            if (toDeletes == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    foreach(var toDelete in toDeletes)
                        collection.DeleteOne(session, filter => filter.Id == toDelete.Id);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        #endregion

        #region Async Method

        public async Task<bool> InsertAsync(T insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(T));

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.InsertOneAsync(session, insert);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public async Task<bool> InsertAsync(IEnumerable<T> toInserts)
        {
            if (toInserts == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.InsertManyAsync(session, toInserts);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public async Task<T> FindAsync(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            var task = await collection.FindAsync(filter => filter.Id == id);
            return task.First();
        }

        public async Task<T> FindAsync(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            var task = await collection.FindAsync(filter => filter.Id == toFind.Id);
            return task.First();
        }

        public async Task<IEnumerable<T>> FindAsync(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.In(f => f.Id, toFinds.Select(toFind => toFind.Id));
            var task = await collection.FindAsync(filter);
            return task.ToList();
        }

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.ReplaceOneAsync(session, filter => filter.Id == toUpdate.Id, toUpdate);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> toUpdates)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.DeleteOneAsync(session, filter => filter.Id == id);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            if (toDelete == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.DeleteOneAsync(session, filter => filter.Id == toDelete.Id);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> toDeletes)
        {
            if (toDeletes == null)
                throw new ArgumentNullException();

            var collection = Context.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.In(f => f.Id, toDeletes.Select(d => d.Id));

            using (var session = Context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    await collection.DeleteManyAsync(session, filter);
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    session.AbortTransaction();
                    return false;
                }
            }
        }

        #endregion

    }
}