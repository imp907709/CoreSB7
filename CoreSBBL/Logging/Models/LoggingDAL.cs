﻿using CoreSBShared.Universal.Infrastructure.Models;

namespace CoreSBShared.Universal.Models
{
    public class LoggingDAL : EntityIntId
    {
        public string Message { get; set; }
    }
}