﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VisionAiServiceDemoApp
{
    public class TokenGenerateRequest
    {
        public string Password { get; set; }
        public string TenantName { get; set; }
        public string DeviceName { get; set; }
        public string User { get; set; }
    }
}
