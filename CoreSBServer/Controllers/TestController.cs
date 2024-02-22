using System.Threading.Tasks;
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
        [Route("test")]
        public async Task<ActionResult> Test()
        {
            await Task.Delay(1);
            return Ok("up and running");
        }
    }
}
