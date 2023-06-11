using CoreSBBL.Logging.Infrastructure.TC;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSBBL.Logging.Infrastructure.TC
{
    //Method level store, typecontaining id 
    public class LogsEFStore : EFStore, ILogsEFStore
    {
        public LogsEFStore(LogsContextTC logsContextTC) : base(logsContextTC)
        {
        }
    }
}

namespace CoreSBBL.Logging.Infrastructure.GN
{
    //Class level store generic id
    public class LogsEFStoreG<T,K> : EFStoreG<T, K>,
        ILogsEFStoreG<T, K>
        where T : class, ICoreDal<K>
    {
        public LogsEFStoreG(LogsContextGN dbContext) : base(dbContext)
        {
        }
    }
    
    //class level store generic id int
    public class LogsEFStoreGInt : EFStoreGInt, ILogsEFStoreGInt
    {
        public LogsEFStoreGInt(DbContext dbContext) : base(dbContext)
        {
        }
    }
    
    
    //method level store generic id int
    public class LogsEFStoreG : EFStoreG, ILogsEFStoreG
    {
        public LogsEFStoreG(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
