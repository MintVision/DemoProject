using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp
{
    public class TokenGenerateRequest
    {
        public string Password { get; set; }
        public string TenantName { get; set; }
        public string DeviceName { get; set; }
    }
}
