using MintIIVisionServiceDemoApp.Requests;
using MintIIVisionServiceDemoApp.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MintIIVisionServiceDemoApp.Demos
{
    /// <summary>
    /// Registering a person using Mint Vision Service
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Demonstrates the process of registering a person i.e. create, add faces and add custom rows of data
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        public static void PersonDemo(RestClient client, string token)
        {     
            var personID = Person.CreatePerson(client, token);
            Person.AddFaceToPerson(client, token, personID, true);
            Person.CreatePersonData(client, token, personID); //not required for registration
        }

        /// <summary>
        /// Creates a New Person 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string CreatePerson(RestClient client, string token)
        {
            var request = new RestRequest($"persons", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            var dateOfBirth = DateTime.Now.AddYears(-20);
            request.AddJsonBody(new CreatePersonRequest() { Name = "Test Demo Person", DateOfBirth = dateOfBirth }); ;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content;

                CreatePersonResponse result = JsonConvert.DeserializeObject<CreatePersonResponse>(message);
                Console.WriteLine("Person Demo Result: ");
                Console.WriteLine($"Person is created successfully with Person ID: {result.PersonId}");
                return result.PersonId;
            }
            else
            {
                var error = response.ErrorMessage;
                Console.WriteLine(error);
                return string.Empty;
            }
        }


        /// <summary>
        /// Adds a face to the person 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="personID"></param>
        /// <param name="retrainPersonGroup"></param>
        /// <param name="imagePath"></param>
        public static void AddFaceToPerson(RestClient client, string token, string personID, bool retrainPersonGroup)
        {
            var imagePath = Path.Join(AppContext.BaseDirectory, $"TestData\\Face.jpg");

            var request = new RestRequest($"persons/{personID}/faces?retrainPersonGroup={retrainPersonGroup}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "multipart/form-data");

            byte[] data = GetByteArrayFromPath(imagePath);
            using var newStream = new MemoryStream(data);
            request.Files.Add(new FileParameter()
            {
                Name = "image",
                Writer = (s) =>
                {
                    newStream.CopyTo(s);
                },
                FileName = "test.jpg",
                ContentLength = newStream.Length
            });
            var response = client.Execute(request);

             if (response.IsSuccessful)
             {
                var message = response.Content;

                Console.WriteLine("Result: ");
                AddFacesToPersonResponse result = JsonConvert.DeserializeObject<AddFacesToPersonResponse>(message);
                Console.WriteLine($"Face Id: {result.FaceId}");
                Console.WriteLine($"Registration Percentage: {result.RegistrationPercentage}");
                Console.WriteLine($"Missing HeadPoses: {JsonConvert.SerializeObject(result.Missingheadposes)}");
             }
            else
            {
                var error = response.Content;
                Console.WriteLine(error);
            }
        }
        /// <summary>
        /// Adds person data to a person
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="personID"></param>
        public static void CreatePersonData(RestClient client, string token, string personID)
        {
            var request = new RestRequest($"persons/{personID}/data", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            request.AddJsonBody(new CreatePersonDataRequest() { JsonData = "{'data': 'Demo Json data'}", Type = "json" }); ;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var message = response.Content;
                CreatePersonDataResponse result = JsonConvert.DeserializeObject<CreatePersonDataResponse>(message);
                Console.WriteLine("Person data is added successfully. Result: ");
                Console.WriteLine($"Person ID: {personID}");
                Console.WriteLine($"Person Data ID: {result.PersonDataId}");
            }
            else
            {
                var error = response.ErrorMessage;
                Console.WriteLine(error);
            }
        }
        public static byte[] GetByteArrayFromPath(string path)
        {
            byte[] image = (new WebClient()).DownloadData(path);
            return image;         
        }
        
    }
}
