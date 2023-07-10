using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSBBL.Logging.Infrastructure.TS;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Models.DAL.GN;
using CoreSBBL.Logging.Models.TC.BL;
using CoreSBBL.Logging.Models.DAL.TS;
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
            ILogsEFStoreG<LoggingDalEfInt, int> logsEFStoreGInt,
            ILogsEFStoreG logsStoreGMethod, 
            
            ILogsEFStore logsStore,
            
            ILogsEFStoreGInt logsStoreGInt,
         
            ILoggingMongoStore mongoStore, IElasticStoreNest elasticStore)
        {
            _logsEFStoreGInt = logsEFStoreGInt;
            _logsStoreGMethod = logsStoreGMethod;
            
            _logsStore = logsStore;
            _logsStoreGInt = logsStoreGInt;

            _mongoStore = mongoStore;
            _elasticStore = elasticStore;
        }
        
        //GN
        //Class level
        private readonly ILogsEFStoreG<LoggingDalEfInt, int> _logsEFStoreGInt;
        //GN
        //Method lvl
        private readonly ILogsEFStoreG _logsStoreGMethod;
        
        //CoreSBBL.Logging.Models.DAL.TC
        //TS via GN
        //Method lvl
        private readonly ILogsEFStore _logsStore;

        // TS via GN
        // class lvl
        private readonly ILogsEFStoreGInt _logsStoreGInt;
        
        private IMongoStore _mongoStore { get; }
        private IElasticStoreNest _elasticStore { get; }
        
        public async Task<LogsBL> AddToAll(LogsBL item)
        {
            _logsStore.CreateDB();

            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            
            
            //GN id Class level
            LoggingDalEfInt _itemGN1 = new() {Message = item.Message};
            var respG = await _logsEFStoreGInt.AddAsync(_itemGN1);
            
            //GN id method lvl
            LoggingDalEfInt _itemGN3 = new() {Message = item.Message};
            var respGi = await _logsStoreGMethod.AddAsync<LoggingDalEfInt,int>(_itemGN3);
            
            //TS via GN method lvl
            LogsDALEf _item = new() {Message = item.Message};
            var resp = await _logsStore.AddAsync(_item);
            
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
