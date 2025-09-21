using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class CheckController : ControllerBase
    {
        public CheckController()
        {
        }

        public async Task<ActionResult> CheckSync()
        {
            await Task.Yield();
            return Ok();
        }

        [HttpGet]
        [Route("pureSyncHttpCall")]
        public ActionResult PureSyncHttpCallForTesting()
        {
            var res = CheckLibs.FakeSyncHttpCall();
            return Ok(res);
        }
        
        [HttpGet]
        [Route("syncCallCPU")]
        public ActionResult PureSyncCPUCallForTesting()
        {
            var res = CheckLibs.pureSyncCPU();
            return Ok(res);
        }
        
        [HttpGet]
        [Route("asyncOverSyncCallCPU")]
        public async Task<ActionResult> AsyncOverSyncCallCpu()
        {
            var res = await CheckLibs.AsyncOverSyncCPU();
            return Ok(res);
        }




        [HttpGet]
        [Route("FakeSyncFromResult")]
        public async Task<ActionResult> FakeSyncFromResult()
        {
            var res = await CheckLibs.FakeSyncFromResult();
            return Ok(res);
        }

        [HttpGet]
        [Route("FakeSyncValueTask")]
        public async Task<IActionResult> FakeSyncValueTask()
        {
            var res = await CheckLibs.FakeSyncValueTask();
            return Ok(res);
        }

        [HttpGet]
        [Route("FakeSyncYield")]
        public async Task<IActionResult> FakeSyncYield()
        {
            var res = await CheckLibs.FakeSyncYield();
            return Ok(res);
        }

        
        
        [HttpGet]
        [Route("FakeSyncTask")]
        public async Task<IActionResult> FakeSyncTask()
        {
            var res = await CheckLibs.FakeSyncTaskRun();
            return Ok(res);
        }

        [HttpGet]
        [Route("FakeSyncTaskThrottle")]
        public async Task<IActionResult> FakeSyncTaskThrottle()
        {
            var res = await CheckLibs.FakeSyncTaskRunThrottle();
            return Ok(res);
        }

        [HttpGet]
        [Route("threadPoolStats")]
        public IActionResult GetThreadPoolStats()
        {
            var stats = CheckLibs.GetThreadPoolStats();
            return Ok(stats);
        }

        [HttpGet]
        [Route("GetLinqRnd")]
        public IActionResult GetLinqRnd()
        {
            var res = CheckLibs.LinqRnd();
            return Ok(res);
        }
    }
}
