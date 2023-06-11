using System;
using System.Threading.Tasks;
using CoreSBBL.Logging.Infrastructure.EF;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Models.BL;
using CoreSBBL.Logging.Models.DAL;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Elastic;
using CoreSBShared.Universal.Infrastructure.Mongo;

namespace CoreSBBL.Logging.Services
{
    public class LoggingService : ILoggingService
    {
        public LoggingService(ILogsEFStore store, ILoggingMongoStore mongoStore, IElasticStoreNest elasticStore)
        {
            _efStore = store;
            _mongoStore = mongoStore;
            _elasticStore = elasticStore;
        }

        private IEFStore _efStore { get; }
        private IMongoStore _mongoStore { get; }
        private IElasticStoreNest _elasticStore { get; }

        public async Task<LogsBL> AddToAll(LogsBL item)
        {
            _efStore.CreateDB();

            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            LogsDALEF _item = new() {Message = item.Message};

            var resp = await _efStore.AddAsync(_item);
            var resp2 = await _mongoStore.AddAsync(_item);

            LogsElastic call = new() {Message = _item?.Message ?? DefaultModelValues.Logging.MessageEmpty};
            try
            {
                var res = _elasticStore.CreateindexIfNotExists<LogsElastic>(DefaultConfigurationValues
                    .DefaultElasticIndex);
                var respelk = await _elasticStore.AddAsyncElk(call);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new LogsBL {Message = resp.Message + "  " + resp2.Message + "  " + call.Message};
        }
    }
}
