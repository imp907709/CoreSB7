using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Models;
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
            var resp = await _loggingService.AddOne(new LoggingBL(){Message = "test"});

            return Ok(resp);
        }
    }
}