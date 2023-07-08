using CoreSBBL.Logging.Infrastructure.GN;
using CoreSBBL.Logging.Infrastructure.TS;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSBBL.Logging.Infrastructure.GN
{
    //GN
    //Class level
    public class LogsEFStoreG<T,K> : EFStoreG<T, K>,
        ILogsEFStoreG<T, K>
        where T : class, ICoreDal<K>
    {
        public LogsEFStoreG(LogsContextGN dbContext) : base(dbContext)
        {
        }
    }
    
    //GN
    //Method lvl
    public class LogsEFStoreG : EFStoreG, ILogsEFStoreG
    {
        public LogsEFStoreG(LogsContextGN dbContext) : base(dbContext)
        {
        }
    }
    
}

namespace CoreSBBL.Logging.Infrastructure.TS
{
    //TS via GN
    //Method lvl
    public class LogsEFStore : EFStore, ILogsEFStore
    {
        public LogsEFStore(LogsContextTC logsContextTC) : base(logsContextTC)
        {
        }
    }

    //???Incorrect inheritance 
    // !!!failed on EF insert
    // TS via GN
    // class lvl
    //class level store generic id int
    public class LogsEFStoreGInt : EFStoreGInt, ILogsEFStoreGInt
    {
        public LogsEFStoreGInt(LogsContextGN dbContext) : base(dbContext)
        {
        }
    }
}
