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
        public abstract void insertMany(IEnumerable<T> insert);
        public abstract void find(ObjectId id);
        public abstract void find(T find);
        public abstract void findMany(IEnumerable<T> find);
        public abstract void updateOne(T update);
        public abstract void updateMany(IEnumerable<T> update);
        public abstract void deleteOne(ObjectId id);
        public abstract void deleteOne(T delete);
        public abstract void deleteMany(IEnumerable<T> delete);
    }
}