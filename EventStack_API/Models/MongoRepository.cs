using System.Collections.Generic;
using MongoDB.Bson;
using System;
using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.Models
{
    public class MongoRepository<T> : IRepositoryFactory<T> where T : IDbModel
    {
        private MongoDbContext _context { get; set; }

        public MongoRepository(MongoDbContext context)
        {
            _context = context;
        }

        #region Sync Method

        public bool Insert(T toInsert)
        {
            if (toInsert == null)
                throw new ArgumentNullException(nameof(T));

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    collection.InsertOne(session, toInsert);
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

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

            var collection = _context.GetCollection<T>(typeof(T).Name);
            return collection.Find(filter => filter.Id == id).First();
        }

        public T Find(T toFind)
        {
            if (toFind == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);
            return collection.Find(filter => filter.Id == toFind.Id).First();
        }

        public IEnumerable<T> Find(IEnumerable<T> toFinds)
        {
            if (toFinds == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);
            var filters = new List<Func<T, bool>>();

            foreach (var toFind in toFinds)
                filters.Add(filter => filter.Id == toFind.Id);

            var result = new List<T>();

            foreach (var filter in filters)
                result.Add(collection.Find(finded => filter.Equals(finded)).First());

            return result;
        }

        public bool Update(T toUpdate)
        {
            if (toUpdate == null)
                throw new ArgumentNullException();

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

            var collection = _context.GetCollection<T>(typeof(T).Name);

            using (var session = _context.MongoClient.StartSession())
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

        public bool InsertAsync(T insert)
        {
            throw new NotImplementedException();
        }

        public bool InsertAsync(IEnumerable<T> toInserts)
        {
            throw new NotImplementedException();
        }

        public T FindAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public T FindAsync(T toFind)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAsync(IEnumerable<T> toFinds)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAsync(T toUpdate)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAsync(IEnumerable<T> toUpdates)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAsync(T toDelete)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAsync(IEnumerable<T> toDelete)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}