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

            var request = new RestRequest("verify", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            request.AddJsonBody(new VerifyRequest() { Email = email, FullName = fullName, SendEmail = false });

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content;
                VerificationResponse result = JsonConvert.DeserializeObject<VerificationResponse>(message);
                Console.WriteLine("Verification Demo Result: ");
                Console.WriteLine($"Verification Link: {result.VerificationLink}");
                Console.WriteLine($"Verification Id: {result.VerificationId}");
            }
            else
            {
                var error = response.ErrorMessage;
                Console.WriteLine(error);
            }
        }
    }
}
