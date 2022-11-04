using MintIIVisionServiceDemoApp.Requests;
using MintIIVisionServiceDemoApp.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Demos
{
    /// <summary>
    /// Creates a verification request for a user using Mint Vision Service
    /// </summary>
    public class Verification
    {
        /// <summary>
        /// Creates a verification request for a user. The resulting link can be opened in a browser on any device
        /// </summary>
        public static void VerifyPerson(RestClient client, string token)
        {
            const string fullName = ""; //TODO: person to be verified
            const string email = ""; //TODO: email of person to be verified

            if (string.IsNullOrEmpty(fullName))
                Console.WriteLine("Verify demo cannot be proceeded. Full name and email is required");

            var request = new RestRequest("verify", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            request.AddJsonBody(new VerifyRequest() { Email = email, FullName = fullName, SendEmail = true, VerifyType = "SendLink" });

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content;
                VerificationResponse result = JsonConvert.DeserializeObject<VerificationResponse>(message);
                Console.WriteLine("Inititate a Verification Demo Result: ");
                Console.WriteLine($"Verification Link: {result.VerifyLink}");
                Console.WriteLine($"Verification Id: {result.VerifyId}");
            }
            else
            {
                var error = response.ErrorMessage;
                var content = response.Content;
                Console.WriteLine(error);
                Console.WriteLine(content);
            }
        }

        /// <summary>
        /// Returns verification result
        /// </summary>
        public static void VerifyResult(RestClient client, string token)
        {
            string verifyId = ""; //TODO: verify Id of the verification process that needs to be viewed

            if(string.IsNullOrEmpty(verifyId))
                Console.WriteLine("Verify demo cannot be proceeded. Verify Id is required");

            var request = new RestRequest($"verify/{verifyId}/result", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content;
                VerificationResponse result = JsonConvert.DeserializeObject<VerificationResponse>(message);

                Console.WriteLine("View a Verification Process Details Demo Result: ");
                Console.WriteLine($"Verification Id: {verifyId}");
                Console.WriteLine($"Verification Result: {message}");
                Console.WriteLine();
            }
            else
            {
                var error = response.ErrorMessage;
                var content = response.Content;
                Console.WriteLine(error);
                Console.WriteLine(content);
            }
        }
    }
}
