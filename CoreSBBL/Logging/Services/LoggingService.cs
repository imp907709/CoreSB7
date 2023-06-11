using System;
using System.Threading.Tasks;
using CoreSBBL.Logging.Infrastructure.TC;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Models.DAL.GN;
using CoreSBBL.Logging.Models.TC.BL;
using CoreSBBL.Logging.Models.DAL.TC;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Elastic;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;
using CoreSBShared.Universal.Infrastructure.Mongo;

namespace CoreSBBL.Logging.Services
{
    public class LoggingService : ILoggingService
    {
        public LoggingService( 
            ILogsEFStore logsStore,
            ILogsEFStoreG<LogsDALEfGn, int> logsEFStoreGInt,
            ILogsEFStoreGInt logsStoreGInt,
            ILogsEFStoreG logsStoreGMethod, 
            ILoggingMongoStore mongoStore, IElasticStoreNest elasticStore)
        {
            _logsStore = logsStore;
            _logsEFStoreGInt = logsEFStoreGInt;
            _logsStoreGInt = logsStoreGInt;
            _logsStoreGMethod = logsStoreGMethod;
            
            
            _mongoStore = mongoStore;
            _elasticStore = elasticStore;
        }
        
        //CoreSBBL.Logging.Models.DAL.TC
        private readonly ILogsEFStore _logsStore;
        private readonly ILogsEFStoreG<LogsDALEfGn, int> _logsEFStoreGInt;
        private readonly ILogsEFStoreGInt _logsStoreGInt;
        private readonly ILogsEFStoreG _logsStoreGMethod;
        
        private IMongoStore _mongoStore { get; }
        private IElasticStoreNest _elasticStore { get; }
        
        public async Task<LogsBL> AddToAll(LogsBL item)
        {
            _logsStore.CreateDB();

            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            LogsDALEf _item = new() {Message = item.Message};
            var resp = await _logsStore.AddAsync(_item);

            LogsDALEfGn _itemGN = new ();
            var respGN = await _logsEFStoreGInt.AddAsync(_itemGN);
            
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
