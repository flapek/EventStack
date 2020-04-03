using System.Collections.Generic;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IRepository<T> where T: IBaseDbModel 
    {
        T insert(T insert);
        IEnumerable<T> insert(IEnumerable<T> insert);
        T find(ObjectId id);
        T find(T find);
        IEnumerable<T> find(IEnumerable<T> find);
        T update(T update);
        IEnumerable<T> update(IEnumerable<T> update);
        bool delete(ObjectId id);
        bool delete(T delete);
        bool delete(IEnumerable<T> delete);
    }
}