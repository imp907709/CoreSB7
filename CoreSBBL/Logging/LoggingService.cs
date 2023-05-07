using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.EF.Infrastructure.Mongo;
using CoreSBShared.Universal.Infrastructure.Mongo;
using CoreSBShared.Universal.Models;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class LoggingService : ILoggingService
    {
        private IEFStore _store { get; set; }
        private IMongoStore _mongoStore { get; set; }
        
        public LoggingService(ILoggingEFStore store, ILoggingMongoStore mongoStore)
        {
            _store = store;
            _mongoStore = mongoStore;
        }

        public async Task<LoggingBL> AddOne(LoggingBL item)
        {
            _store.CreateDB();
            
            _mongoStore.CreateDB();

            LoggingEF _item = new LoggingEF() {Message = item.Message};
            
            var resp = await _store.AddAsync(_item);
            var resp2 = await _mongoStore.AddAsync(_item);
            
            return new LoggingBL() {Message = resp.Message + "  " + resp2.Message};
        }
    }
}