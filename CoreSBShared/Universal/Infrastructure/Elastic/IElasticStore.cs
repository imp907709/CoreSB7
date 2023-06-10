using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
    public interface IElasticStore : IStore
    {
        void SetIndex(string indexName);
        
        Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>;
        Task<T?> GetByIdAsync<T>(string id) where T : class, IEntityStringId;
        Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) where T : class, IEntityStringId;
        Task<T> UpdateAsync<T>(T item) where T : class;
        Task<bool> DeleteAsync<T>(T item) where T : class, IEntityStringId;
        Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, IEntityStringId;
    }
}
