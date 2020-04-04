using System.Collections.Generic;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IRepositoryFactory<T> where T: IDbModel 
    {
        T Insert(T insert);
        IEnumerable<T> Insert(IEnumerable<T> toInsert);
        T Find(ObjectId id);
        T Find(T toFind);
        IEnumerable<T> Find(IEnumerable<T> toFinds);
        T Update(T toUpdate);
        IEnumerable<T> Update(IEnumerable<T> toUpdate);
        bool Delete(ObjectId id);
        bool Delete(T toDelete);
        bool Delete(IEnumerable<T> toDelete);
    }
}