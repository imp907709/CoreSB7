using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;

namespace CoreSBShared.Universal.Models
{
    public class LoggingBL : EntityEF
    {
        public string Message { get; set; }
    }
}