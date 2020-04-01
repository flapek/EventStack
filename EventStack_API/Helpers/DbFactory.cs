using System.Linq;
using System.Collections.Generic;

namespace EventStack_API.Helpers
{
    abstract class DbFactory<T>
    {
        public abstract void insertOne(T insert);
        public abstract void insertMany(IEnumerable<T> insert);
        public abstract void find(string id);
        public abstract void updateOne(T update);
        public abstract void updateMany(IEnumerable<T> update);
        public abstract void deleteOne(string id);
        public abstract void deleteMany(IEnumerable<T> delete);
    }
}