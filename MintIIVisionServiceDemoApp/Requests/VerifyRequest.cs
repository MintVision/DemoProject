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
        public bool ScanIdDocument { get; set; } 
        public bool AskThirdPartyQuestion { get; set; }
        public string ConfirmAction { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public bool SendWhatsApp { get; set; }
      
        public string VerifyType { get; set; }
        public string NotifyEmail { get; set; }
       
        public string RedirectUrl { get; set; }
        public string VerifyLinkId { get; set; }
        public string ExpiryDateTime { get; set; }

    }
}
