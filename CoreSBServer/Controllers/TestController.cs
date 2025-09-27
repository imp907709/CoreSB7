using System.Threading.Tasks;
using CoreSBBL.Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> Test()
        {
            var tsk = await Task.FromResult("up and running !!");
            return Ok(tsk);
        }
    }
}
