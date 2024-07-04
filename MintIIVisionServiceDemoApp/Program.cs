using System;
using RestSharp;
using VisionAiServiceDemoApp.Demos;
using System.Threading.Tasks;
using System.Threading;

namespace VisionAiServiceDemoApp
{
    class Program
    {
        private static RestClient _client = new RestClient("https://service.visionaisuite.net/v2.0/api/");

        //TODO:
        //NB: set your tenant details here!
        private const string tenantKey = ""; 
        private const string tenantName = "";
        private const string deviceName = "DemoDevice";  

        public static void Main(string[] args)
        {
            Console.WriteLine("This is a console app used to demonstrate features of Vision AI Service.");

            //This token is needed to make any calls to the service and can be re-used after generation
            var visionServiceToken = GenerateToken(tenantKey, tenantName, deviceName);
            if (string.IsNullOrEmpty(visionServiceToken))
                Console.WriteLine("Ensure that tenant name and tenant key supplied are correct.");
            else
            {
                UploadAndListImages.UploadImage(_client, visionServiceToken);

                UploadAndListImages.ListImages(_client, visionServiceToken);
                
            }
        }

        /// <summary>
        ///  Generates Token to access Mint Vision Service
        /// </summary>
        private static string GenerateToken(string tenantKey, string tenantName, string deviceName)
        {            
            var request = new RestRequest("token/generate", Method.Post);
            request.AddHeader("Content-Type", "application/json");            

            request.AddJsonBody(new TokenGenerateRequest() { Password = tenantKey, TenantName = tenantName, DeviceName = deviceName, User = "" });

            var response = _client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content.Trim('"');
                Console.WriteLine("Vision service token has been created");
                Console.WriteLine();
                return message;
            }
            else
            {
                var error = response.ErrorMessage;
                Console.WriteLine(error);
                return string.Empty;
            }
        } 
    }
}
 