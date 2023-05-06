using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface IStore
    {
        Task<T> AddAsync<T>(T item) where T : class;
        Task<IEnumerable<T>> AddManyAsync<T>(IEnumerable<T> items) where T : class;
        Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) where T : class;
    }
    
    public interface IEFStore : IStore
    {
        Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>;
        Task<T?> GetByIdAsync<T>(int id) where T : class, IEntityIntId;
        
        Task<T> UpdateAsync<T>(T item) where T : class;
        Task<bool> DeleteAsync<T>(T item) where T : class;
        Task<IEnumerable<T>> DeleteManyAsync<T>(IEnumerable<T> items) where T : class;
    }

    public interface IMongoStore : IStore
    {
        Task<T> GetByIdAsync<T>(T item) where T : class, IEntityObjectId;
        Task<T> UpdateAsync<T>(T item) where T : class, IEntityObjectId;
        Task<bool> DeleteAsync<T>(T item) where T : class, IEntityObjectId;
        Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class,IEntityObjectId;
    }
    
}