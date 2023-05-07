using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class EFStore : IEFStore
    {
        private readonly DbContext _dbContext;

        public EFStore(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : class, IEntityIntId
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<T> AddAsync<T>(T item) where T : class
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> AddManyAsync<T>(IEnumerable<T> items) where T : class
        {
            await _dbContext.Set<T>().AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();
            return items;
        }

        public async Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> UpdateAsync<T>(T item) where T : class
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync<T>(T item) where T : class
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> DeleteManyAsync<T>(IEnumerable<T> items) where T : class
        {
            _dbContext.Set<T>().RemoveRange(items);
            await _dbContext.SaveChangesAsync();
            return items;
        }

        public void CreateDB()
        {
            _dbContext.Database.EnsureCreated();
        }
        
        public void DropDB()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}