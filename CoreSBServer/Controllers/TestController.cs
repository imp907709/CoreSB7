using System.Threading.Tasks;
using CoreSBBL.Logging;
using CoreSBBL.Logging.Models;
using CoreSBBL.Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class TestController : ControllerBase
    {
        private ILoggingService _loggingService;

        public TestController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [HttpGet]
        [Route("Test")]
        public async Task<ActionResult> Test()
        {
            var resp = await _loggingService.AddToAll(new LogsBL() {Message = "test"});

            return Ok(resp);
        }
    }
}
