using System;
using System.Threading.Tasks;
using CoreSBBL.Logging.Infrastructure.EF;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Models;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Elastic;
using CoreSBShared.Universal.Infrastructure.Mongo;

namespace CoreSBBL.Logging.Services
{
    public class LoggingService : ILoggingService
    {
        private IEFStore _store { get; set; }
        private IMongoStore _mongoStore { get; set; }
        private IElasticStoreNest _elasticStore { get; set; }


        public LoggingService(ILoggingEFStore store, ILoggingMongoStore mongoStore, IElasticStoreNest elasticStore)
        {
            _store = store;
            _mongoStore = mongoStore;
            _elasticStore = elasticStore;
        }

        public async Task<LoggingBL> AddToAll(LoggingBL item)
        {
            _store.CreateDB();

            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            LoggingDAL _item = new() {Message = item.Message};

            var resp = await _store.AddAsync(_item);
            var resp2 = await _mongoStore.AddAsync(_item);

            LoggingElastic call = new () {Message = _item?.Message ?? DefaultModelValues.Logging.MessageEmpty};
            try
            {
                var res = _elasticStore.CreateindexIfNotExists<LoggingElastic>(DefaultConfigurationValues.DefaultElasticIndex);
                var respelk = await _elasticStore.AddAsyncElk(call);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new LoggingBL() {Message = resp.Message + "  " + resp2.Message + "  " + call.Message};
        }
    }
}
