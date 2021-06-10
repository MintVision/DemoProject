using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class DocumentUploadResponse: BaseResponse
    {
        /// <summary>
        /// File guid of the document that was uploaded
        /// </summary>
        public string FileGuid { get; set; }
    }
}
