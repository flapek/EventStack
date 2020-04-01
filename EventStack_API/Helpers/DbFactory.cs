using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;

namespace EventStack_API.Helpers
{
    abstract class DbFactory<T>
    {
        public abstract void insertOne(T insert);
        public abstract void insertMany(IEnumerable<T> insert);
        public abstract void find(ObjectId id);
        public abstract void updateOne(T update);
        public abstract void updateMany(IEnumerable<T> update);
        public abstract void deleteOne(ObjectId id);
        public abstract void deleteMany(IEnumerable<T> delete);
    }
}