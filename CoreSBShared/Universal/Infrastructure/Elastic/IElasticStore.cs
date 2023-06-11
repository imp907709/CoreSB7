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

        Task<T> AddAsync<T>(T item) where T : class;
        
        Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, ICoreDal<TKey>;
        Task<T?> GetByIdAsync<T>(string id) where T : class, ICoreDalStrg;
        Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) where T : class, ICoreDalStrg;
        Task<T> UpdateAsync<T>(T item) where T : class;
        Task<bool> DeleteAsync<T>(T item) where T : class, ICoreDalStrg;
        Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, ICoreDalStrg;
    }
}
