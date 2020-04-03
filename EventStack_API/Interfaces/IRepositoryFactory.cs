using System.Collections.Generic;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IRepositoryFactory<T> where T: IDbModel 
    {
        T Insert(T insert);
        IEnumerable<T> Insert(IEnumerable<T> insert);
        T Find(ObjectId id);
        T Find(T find);
        IEnumerable<T> Find(IEnumerable<T> find);
        T Update(T update);
        IEnumerable<T> Update(IEnumerable<T> update);
        bool Delete(ObjectId id);
        bool Delete(T delete);
        bool Delete(IEnumerable<T> delete);
    }
}