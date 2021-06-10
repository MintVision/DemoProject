using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class VerificationResponse: BaseResponse
    {
        /// <summary>
        /// Verification link that was created
        /// </summary>
        public string VerificationLink { get; set; }
        /// <summary>
        /// Verification Id that was created
        /// </summary>
        public string VerificationId { get; set; }

        
}
}
