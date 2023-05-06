using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public interface IEFStore : IStore
    {
        Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>;
        Task<T?> GetByIdAsync<T>(int id) where T : class, IEntityIntId;
        
        Task<T> UpdateAsync<T>(T item) where T : class;
        Task<bool> DeleteAsync<T>(T item) where T : class;
        Task<IEnumerable<T>> DeleteManyAsync<T>(IEnumerable<T> items) where T : class;
    }
}