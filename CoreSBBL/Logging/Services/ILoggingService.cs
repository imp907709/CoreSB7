using System.Threading.Tasks;
using CoreSBBL.Logging.Models;

namespace CoreSBBL.Logging.Services
{
    public interface ILoggingService
    {
        Task<LoggingBL> AddToAll(LoggingBL item);
    }
}
