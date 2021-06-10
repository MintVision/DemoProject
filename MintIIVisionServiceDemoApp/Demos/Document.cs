using MintIIVisionServiceDemoApp.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MintIIVisionServiceDemoApp.Responses;
using Newtonsoft.Json;
using System.Net;

namespace MintIIVisionServiceDemoApp.Demos
{
    /// <summary>
    /// Uploads, classifies and extracts data using Mint Vision Service
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Uploads a document 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        public static void UploadDocument(RestClient client, string token)
        {
            //Load a local file for the demo
            var docPath = Path.Join(AppContext.BaseDirectory, $"TestData\\TestPDF.pdf"); 
            var fileName = Document.GetFileName(docPath);

            //Create the service request
            var request = new RestRequest($"documents/upload?fileName={fileName}&user=Test&classify=true", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "multipart/form-data");
            //Get the byte array for streaming. The service also supports other mechanisms for upload e.g. Base64
            byte[] data = GetByteArrayFromPath(docPath);
            using var newStream = new MemoryStream(data);
            request.Files.Add(new FileParameter()
            {
                Name = "image",
                Writer = (s) =>
                {
                    newStream.CopyTo(s);
                },
                FileName = GetFileName(docPath),
                ContentLength = newStream.Length
            });
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                DocumentUploadResponse result = JsonConvert.DeserializeObject<DocumentUploadResponse>(response.Content);
                Console.WriteLine("Document Demo Result: ");
                Console.WriteLine($"File Guid: { result.FileGuid}");
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
        public static string GetFileName(string input)
        {
            var fileName = Path.GetFileName(input);
            return fileName;           
        }

        
    }
}
