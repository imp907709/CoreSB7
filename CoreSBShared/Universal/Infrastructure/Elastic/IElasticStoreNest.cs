using System.Threading.Tasks;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
    public interface IElasticStoreNest
    {
        Task<IndexResponse> AddAsyncElk<T>(T item) where T : class;
    }
}