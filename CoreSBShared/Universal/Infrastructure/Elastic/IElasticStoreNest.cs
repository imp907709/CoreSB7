using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
    public interface IElasticStoreNest : IElasticStore
    {
        public Task<IndexResponse> AddAsyncElk<T>(T item) where T : class;

        public new Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, IEntityStringId;

        public string CreateindexIfNotExists<T>(string indexName)
            where T : class, IEntityStringId;
    }
}
