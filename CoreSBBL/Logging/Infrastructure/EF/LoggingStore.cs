using CoreSBShared.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.EF
{
    public class LoggingEFStore : EFStore, ILoggingEFStore 
    {
        public LoggingEFStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}