using System.Threading.Tasks;
using CoreSBBL.Logging.Models;

namespace CoreSBBL.Logging.Services
{
    public interface ILoggingService
    {
        Task<LogsBL> AddToAll(LogsBL item);
    }
}
