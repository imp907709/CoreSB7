using Microsoft.EntityFrameworkCore;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class LoggingEFStore : EFStore, ILoggingEFStore 
    {
        public LoggingEFStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}