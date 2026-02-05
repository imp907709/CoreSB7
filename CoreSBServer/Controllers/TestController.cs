using System.Threading.Tasks;
using CoreSBBL.Logging.Services;
using CoreSBShared.Universal.Infrastructure.HTTP.MyApp.Services.Http;
using CoreSBShared.Universal.Infrastructure.Rabbit;
using Microsoft.AspNetCore.Mvc;

namespace CoreSBServer.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IHttpService _http;
        private readonly IRabbitClient _rabbit;
        
        public TestController(IHttpService http, IRabbitClient rabbit) {
            _http = http;
            _rabbit = rabbit;
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> Test()
        {
            var tsk = await Task.FromResult("up and running !!");
            var resp = await _http.GetAsync<string>("https://api.restful-api.dev/objects");
            return Ok(resp);
        }

        [HttpGet]
        [Route("Connected")]
        public async Task<ActionResult> Connected()
        {
            try
            {
                return Ok($"Rabbit is connected : {_rabbit.IsConnected()}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("Channel")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var ch = await _rabbit.ChannelOpen();
                var num = ch.ChannelNumber;
                await _rabbit.ChannelClose(ch);
                return Ok($"Channel : {num}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
