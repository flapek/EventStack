using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;

namespace EventStack_API.Helpers
{
    public abstract class DbFactory<T>
    {
        public abstract T insertOne(T insert);
        public abstract IEnumerable<T> insertMany(IEnumerable<T> insert);
        public abstract T find(ObjectId id);
        public abstract IEnumerable<T> find(T find);
        public abstract IEnumerable<T> findMany(IEnumerable<T> find);
        public abstract T updateOne(T update);
        public abstract IEnumerable<T> updateMany(IEnumerable<T> update);
        public abstract bool deleteOne(ObjectId id);
        public abstract bool deleteOne(T delete);
        public abstract bool deleteMany(IEnumerable<T> delete);
    }
}