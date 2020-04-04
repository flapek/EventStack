using System.Collections.Generic;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IRepositoryFactory<T> where T: IDbModel 
    {
        bool Insert(T insert);
        bool Insert(IEnumerable<T> toInserts);
        T Find(ObjectId id);
        T Find(T toFind);
        IEnumerable<T> Find(IEnumerable<T> toFinds);
        bool Update(T toUpdate);
        bool Update(IEnumerable<T> toUpdates);
        bool Delete(ObjectId id);
        bool Delete(T toDelete);
        bool Delete(IEnumerable<T> toDelete);
    }
}