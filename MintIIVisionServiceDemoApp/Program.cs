using System;
using RestSharp;
using MintIIVisionServiceDemoApp.Demos;

namespace MintIIVisionServiceDemoApp
{
    class Program
    {
        private static RestClient _client = new RestClient("https://mintiivisionservice.azurewebsites.net/v1.6/api/");

        //TODO:
        //NB: set your tenant details here!!!!!!!!!!!!!
        private const string tenantKey = ""; 
        private const string tenantName = "";        
        
        private const string deviceName = "demoProject";

        public static void Main(string[] args)
        {
            Console.WriteLine("This is a console app used to demonstrate features of Mint Vision Service.");
            //This token is needed to make any calls to the service and can be re-used after generation
            var visionServiceToken = GenerateToken(tenantKey, tenantName, deviceName);
            if (string.IsNullOrEmpty(visionServiceToken))
                Console.WriteLine("Ensure that tenant name and tenant key supplied are correct.");
            else
            {
                Console.WriteLine("Please enter the demo which you would like to see. \n1.Verification Demo \n2.Documents Demo \n3.Person Demo \n4.Cancel");

                var userAnswer = Console.ReadLine();
                if (userAnswer == "1")
                {
                    Console.WriteLine("This demonstrates how a verification check is sent against a person using MintVision Service");
                    Verification.VerifyPerson(_client, visionServiceToken);
                }
                else if (userAnswer == "2")
                {
                    Console.WriteLine("This demonstrates how a document is uploaded, classified and data extracted using MintVision Service");
                    Document.UploadDocument(_client, visionServiceToken);
                }
                else if (userAnswer == "3")
                {
                    Console.WriteLine("This demonstrates how a person is registered using MintVision Service");
                    Person.PersonDemo(_client, visionServiceToken);
                }
                else
                {
                    Console.WriteLine("Press any key to cancel");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        ///  Generates Token to access Mint Vision Service
        /// </summary>
        private static string GenerateToken(string tenantKey, string tenantName, string deviceName)
        {            
            var request = new RestRequest("Token/Generate", Method.POST);
            request.AddHeader("Content-Type", "application/json");            

            request.AddJsonBody(new TokenGenerateRequest() { Password = tenantKey, TenantName = tenantName, DeviceName = deviceName });

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
 