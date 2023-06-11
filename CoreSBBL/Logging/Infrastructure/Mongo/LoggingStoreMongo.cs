using CoreSBShared.Universal.Infrastructure.Mongo;

namespace CoreSBBL.Logging.Infrastructure.Mongo
{
    public class LoggingMongoStore : MongoStore, ILoggingMongoStore
    {
        public LoggingMongoStore(string connString, string dbName) : base(connString, dbName)
        {
        }
    }
}
