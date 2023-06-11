using System.Threading.Tasks;
using CoreSBBL.Logging.Models.TC.BL;

namespace CoreSBBL.Logging.Services
{
    public interface ILoggingService
    {
        Task<LogsBL> AddToAll(LogsBL item);
    }
}
