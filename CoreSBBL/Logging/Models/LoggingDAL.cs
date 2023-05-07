using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;

namespace CoreSBShared.Universal.Models
{
    public class LoggingEF : EntityEF
    {
        public string Message { get; set; }
    }
    
    public class LoggingMongo : EntityMongo
    {
        public string Message { get; set; }
    }
    
    public class LoggingElastic : EntityElastic
    {
        public string Message { get; set; }
    }
}