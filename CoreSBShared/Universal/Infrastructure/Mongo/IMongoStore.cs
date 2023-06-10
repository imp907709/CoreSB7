using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;

namespace CoreSBShared.Universal.Infrastructure.Mongo
{
    public interface IMongoStore : IStore
    {
        Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) 
            where T : class, IEntityObjectId;
        Task<T> GetByIdAsync<T>(T item) where T : class, IEntityObjectId;
        Task<T> UpdateAsync<T>(T item) where T : class, IEntityObjectId;
        Task<bool> DeleteAsync<T>(T item) where T : class, IEntityObjectId;
        Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, IEntityObjectId;
    }
}
