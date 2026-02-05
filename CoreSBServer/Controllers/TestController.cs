using System.Threading.Tasks;
using CoreSBBL.Logging.Services;
using CoreSBShared.Universal.Infrastructure.HTTP.MyApp.Services.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IHttpService _http;
        public TestController(IHttpService http) {
            _http = http;
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> Test()
        {
            var tsk = await Task.FromResult("up and running !!");
            var resp = await _http.GetAsync<string>("https://api.restful-api.dev/objects");
            return Ok(resp);
        }
    }
}
