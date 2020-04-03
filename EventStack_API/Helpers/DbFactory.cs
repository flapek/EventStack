using System.Collections.Generic;
using MongoDB.Bson;

namespace EventStack_API.Helpers
{
    public abstract class DbFactory<T>
    {
        public abstract T insert(T insert);
        public abstract IEnumerable<T> insert(IEnumerable<T> insert);
        public abstract T find(ObjectId id);
        public abstract T find(T find);
        public abstract IEnumerable<T> find(IEnumerable<T> find);
        public abstract T update(T update);
        public abstract IEnumerable<T> update(IEnumerable<T> update);
        public abstract bool delete(ObjectId id);
        public abstract bool delete(T delete);
        public abstract bool delete(IEnumerable<T> delete);
    }
}