using System.Threading.Tasks;
using CoreSBShared.Universal.Models;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public interface ILoggingService
    {
        Task<LoggingBL> AddOne(LoggingBL item);
    }
}