using System.Threading.Tasks;
using CoreSBBL.Logging;
using CoreSBBL.Logging.Models;
using CoreSBBL.Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class LoggingController : ControllerBase
    {
        private ILoggingService _loggingService;
        public LoggingController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [HttpGet]
        [Route("AddToAll")]
        public async Task<ActionResult> AddToAll(LoggingAPI item)
        {
            var resp = await _loggingService.AddToAll(new LoggingBL(){Message = "test"});

            return Ok(resp);
        }
    }
}
