using System.Threading.Tasks;
using CoreSBBL.Logging.Models.TC.API;
using CoreSBBL.Logging.Models.TC.BL;
using CoreSBBL.Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class LoggingController : ControllerBase
    {
        private readonly ILoggingService _loggingService;

        public LoggingController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [HttpPost]
        [Route("AddToAll")]
        public async Task<ActionResult> AddToAll(LogsAPI item)
        {
            var resp = await _loggingService.AddToAll(new LogsBL {Message = item.Message});

            return Ok(resp);
        }
    }
}
