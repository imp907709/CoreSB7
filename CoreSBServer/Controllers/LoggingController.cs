﻿using System.Threading.Tasks;
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

        [HttpPost]
        [Route("AddToAll")]
        public async Task<ActionResult> AddToAll(LogsAPI item)
        {
            var resp = await _loggingService.AddToAll(new LogsBL() {Message = item.Message});

            return Ok(resp);
        }
    }
}
