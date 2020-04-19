using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStack_API.Interfaces
{
    public interface IRepositoryFactory<T> where T: IDbModel 
    {
        bool Insert(T insert);
        Task<bool> InsertAsync(T insert);
        bool Insert(IEnumerable<T> toInserts);
        Task<bool> InsertAsync(IEnumerable<T> toInserts);
        T Find(string id);
        Task<T> FindAsync(string id);
        T Find(T toFind);
        Task<T> FindAsync(T toFind);
        IEnumerable<T> Find(IEnumerable<T> toFinds);
        Task<IEnumerable<T>> FindAsync(IEnumerable<T> toFinds);
        bool Update(string id, T toUpdate);
        Task<bool> UpdateAsync(string id,T toUpdate);
        bool Update(IEnumerable<T> toUpdates);
        Task<bool> UpdateAsync(IEnumerable<T> toUpdates);
        bool Delete(string id);
        Task<bool> DeleteAsync(string id);
        bool Delete(T toDelete);
        Task<bool> DeleteAsync(T toDelete);
        bool Delete(IEnumerable<T> toDelete);
        Task<bool> DeleteAsync(IEnumerable<T> toDeletes);
    }
}