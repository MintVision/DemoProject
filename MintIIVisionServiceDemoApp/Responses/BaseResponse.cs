using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class BaseResponse
    {
        public string TransactionId { get; set; }
        public string ElapsedMilliseconds { get; set; }
        public string IsObsoleteServiceVersion { get; set; }
        public string ApiVersion { get; set; }
    }
}
