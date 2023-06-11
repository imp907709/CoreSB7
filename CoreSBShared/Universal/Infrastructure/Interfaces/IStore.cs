using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface IStore
    {
        Task<T> AddAsync<T>(T item) where T : class;
        Task<IEnumerable<T>> AddManyAsync<T>(IEnumerable<T> items) where T : class;


        public void CreateDB();
        public void DropDB();
    }
}
