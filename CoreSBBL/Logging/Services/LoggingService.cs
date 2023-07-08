using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSBBL.Logging.Infrastructure.TS;
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
        private readonly ILogsEFStoreG _logsStoreGMethod;
        
        private readonly ILogsEFStoreGInt _logsStoreGInt;
        
        private IMongoStore _mongoStore { get; }
        private IElasticStoreNest _elasticStore { get; }
        
        public async Task<LogsBL> AddToAll(LogsBL item)
        {
            _logsStore.CreateDB();

            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            // type containing id store
            LogsDALEf _item = new() {Message = item.Message};
            var resp = await _logsStore.AddAsync(_item);

            // generic int id store
            LogsDALEfGn _itemGN1 = new() {Message = item.Message};
            var respG = await _logsEFStoreGInt.AddAsync(_itemGN1);
            
            LogsDALEfGn _itemGN3 = new() {Message = item.Message};
            var respGi = await _logsStoreGMethod.AddAsync<LogsDALEfGn,int>(_itemGN3);
            
            
            
            //needed seperate model registration, inheritance err
            // LogsDALEfGn _itemGN2 = new() {Message = item.Message};
            // var respGint = await _logsStoreGInt.AddAsync(_itemGN2);
            
           
                
            LogsMongo _itemMng = new () { Message = item.Message 
                ,Label = new LabelMongo(){Text= "label 1" }
                ,Tags = new List<TagMongo>(){new (){Text = "tag 1"},new (){Text = "tag 2"}}};
            var resp2 = await _mongoStore.AddAsync(_itemMng);

            LogsElk _call = new () { Message = item.Message ?? DefaultModelValues.Logging.MessageEmpty 
                ,Label = new LabelElk(){Text= "label 1" }
                ,Tags = new List<TagElk>(){new (){Text = "tag 1"},new (){Text = "tag 2"}}};
            try
            {
                var res = _elasticStore.CreateindexIfNotExists<LogsElk>(DefaultConfigurationValues
                    .DefaultElasticIndex);
                var respelk = await _elasticStore.AddAsyncElk(_call);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new LogsBL {Message = resp.Message + "  " + resp2.Message + "  " + _call.Message};
        }
    }
}
