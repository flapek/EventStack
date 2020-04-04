using System.Collections.Generic;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IRepositoryFactory<T> where T: IDbModel 
    {
        bool Insert(T insert);
        bool InsertAsync(T insert);
        bool Insert(IEnumerable<T> toInserts);
        bool InsertAsync(IEnumerable<T> toInserts);
        T Find(ObjectId id);
        T FindAsync(ObjectId id);
        T Find(T toFind);
        T FindAsync(T toFind);
        IEnumerable<T> Find(IEnumerable<T> toFinds);
        IEnumerable<T> FindAsync(IEnumerable<T> toFinds);
        bool Update(T toUpdate);
        bool UpdateAsync(T toUpdate);
        bool Update(IEnumerable<T> toUpdates);
        bool UpdateAsync(IEnumerable<T> toUpdates);
        bool Delete(ObjectId id);
        bool DeleteAsync(ObjectId id);
        bool Delete(T toDelete);
        bool DeleteAsync(T toDelete);
        bool Delete(IEnumerable<T> toDelete);
        bool DeleteAsync(IEnumerable<T> toDelete);
    }
}