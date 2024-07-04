using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VisionAiServiceDemoApp.Demos
{
    /// <summary>
    /// Uploads an image to the service and lists images
    /// </summary>
    internal class UploadAndListImages
    {
        public static void UploadImage(RestClient client, string token)
        {
            Console.WriteLine("Call the service with an image");
            //Load a local file for the demo
            var imagePath = Path.Join(AppContext.BaseDirectory, $"TestData\\DALLETestImage.jpg");

            //Call the service to upload the document
            var request = new RestRequest($"images/upload", Method.Post);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "multipart/form-data");

            //Add the image as a multi-part form field
            request.AddFile("image", imagePath, ContentType.Binary);
            var response = client.Execute(request);           

            //check response
            if (response.IsSuccessful)
            {
                //Image received
                Console.WriteLine("Success!");
                Console.WriteLine(response.Content);

                //At this point the image can be processed - this will require a tracker to be set up
            }
            else
            {
                //Document failed to upload
                Console.WriteLine(response.ErrorMessage);
                Console.WriteLine(response.Content);
            }

        }


        public static void ListImages(RestClient client, string token)
        {
            Console.WriteLine("Call the service to get a list of images");

            //Call the service to upload the document
            var request = new RestRequest($"images", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            // Create the request object. Can add filter to see specific classes or images from a specific source
            var listTrackedImagesRequest = new ListTrackedImagesRequest
            {
                ViewType= "Unclassified", //Other views can show images from a specific tracker or needing review
                MaxRows=3, //Return 3 images
            };

            // Serialize the request object to JSON
            var jsonRequest = JsonConvert.SerializeObject(listTrackedImagesRequest);

            // Add the JSON payload as a query parameter
            request.AddBody( jsonRequest, ContentType.Json);

            var response = client.Execute(request);

            //check response
            if (response.IsSuccessful)
            {
                //Image received
                Console.WriteLine("Success!");

                // Parse and pretty-print the JSON response
                var jsonResponse = JToken.Parse(response.Content);
                var formattedJson = jsonResponse.ToString(Formatting.Indented);
                Console.WriteLine(formattedJson);
            }
            else
            {
                //Document failed to upload
                Console.WriteLine(response.ErrorMessage);
                Console.WriteLine(response.Content);
            }

        }

    }
}
