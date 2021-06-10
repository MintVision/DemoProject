using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class CreatePersonResponse: BaseResponse
    {
        /// <summary>
        /// Id of the person that was created
        /// </summary>
        public string PersonId { get; set; }     
    }
}
