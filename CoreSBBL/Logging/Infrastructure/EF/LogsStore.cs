using CoreSBShared.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.EF
{
    public class LogsEFStore : EFStore, ILogsEFStore
    {
        public LogsEFStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
