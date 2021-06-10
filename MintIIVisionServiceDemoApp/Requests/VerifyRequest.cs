using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Requests
{
    public class VerifyRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string RequestedByUserId { get; set; }
        public string RequestedByName { get; set; }
        public string AdditionalQuestion { get; set; }
        public string AdditionalAnswer { get; set; }
        public bool ScanIdDocument { get; set; } 
        public bool AskThirdPartyQuestion { get; set; }
        public bool SendEmail { get; set; }

    }
}
