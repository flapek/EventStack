using System.Collections.Generic;

namespace EventStack_API.Helpers
{
    abstract class AbstractFactory<T>
    {
        public abstract void insertOne(T insert);
        public abstract void insertMany(List<T> insert);
        public abstract void find(string id);
        public abstract void updateOne(T update);
        public abstract void updateMany(List<T> update);
        public abstract void deleteOne(string id);
        public abstract void deleteMany(List<T> delete);
    }
}