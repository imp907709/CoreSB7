﻿using System;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.EF.Infrastructure.Mongo;
using CoreSBShared.Universal.Infrastructure.Elastic;
using CoreSBShared.Universal.Infrastructure.Mongo;
using CoreSBShared.Universal.Models;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class LoggingService : ILoggingService
    {
        private IEFStore _store { get; set; }
        private IMongoStore _mongoStore { get; set; }
        private IElasticStore _elasticStore { get; set; }
        
        
        public LoggingService(ILoggingEFStore store, ILoggingMongoStore mongoStore, IElasticStore elasticStore)
        {
            _store = store;
            _mongoStore = mongoStore;
            _elasticStore = elasticStore;
        }

        public async Task<LoggingBL> AddOne(LoggingBL item)
        {
            _store.CreateDB();
            
            _mongoStore.CreateDB();

            _elasticStore.CreateDB();

            LoggingEF _item = new LoggingEF() {Message = item.Message};
            
            var resp = await _store.AddAsync(_item);
            var resp2 = await _mongoStore.AddAsync(_item);

            LoggingElastic call = new LoggingElastic(){Message = _item.Message};
            LoggingElastic resp3 = new();
            try
            {
                var res = _elasticStore.CreateindexIfNotExists<LoggingElastic>("logging");
                resp3 = await _elasticStore.AddAsync(call);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new LoggingBL() {Message = resp.Message + "  " + resp2.Message + "  " + resp3.Message};
        }
    }
}