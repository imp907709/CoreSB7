using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
    public interface IElasticStoreNest : IElasticStore
    {
        public Task<IndexResponse> AddAsyncElk<T>(T item) where T : class;

        public string CreateindexIfNotExists<T>(string indexName)
            where T : class, IEntityStringId;
    }
}
