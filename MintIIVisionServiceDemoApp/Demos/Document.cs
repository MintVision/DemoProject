using MintIIVisionServiceDemoApp.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MintIIVisionServiceDemoApp.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace MintIIVisionServiceDemoApp.Demos
{
    /// <summary>
    /// Uploads, classifies and extracts data using Mint Vision Service
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Uploads a document, polls for the result and fetches the document when done processing
        /// </summary>
        public static void UploadAndProcessDocument(RestClient client, string token)
        {
            //Load a local file for the demo
            var docPath = Path.Join(AppContext.BaseDirectory, $"TestData\\TestPDF.pdf");
            var fileName = Document.GetFileName(docPath);
            
            //Call the service to upload the document
            IRestResponse response=UploadDocument(client, token, docPath, fileName);

            if (response.IsSuccessful)
            {
                //Document received
                DocumentUploadResponse result = JsonConvert.DeserializeObject<DocumentUploadResponse>(response.Content);
                Console.WriteLine("Document Demo Result: ");
                Console.WriteLine($"File Guid: {result.FileGuid}");

                //Poll until finished processing
                Task.Delay(2000).Wait();
                string getDocumentStatus = GetDocumentStatus(client, token, result.FileGuid);
                Console.WriteLine($"Checking Document's {result.FileGuid} status: {getDocumentStatus}");
                while (getDocumentStatus == "UploadReceived" || getDocumentStatus == "SentForProcessing"
                    || getDocumentStatus=="Uploaded")
                {
                    getDocumentStatus = GetDocumentStatus(client, token, result.FileGuid);
                    Task.Delay(2000).Wait();
                }

                //Fetch processed document details
                ViewDocument(client, token, result.FileGuid);
            }
            else
            {
                //Document failed to upload
                Console.WriteLine(response.ErrorMessage);
                Console.WriteLine(response.Content);
            }
        }

        private static IRestResponse UploadDocument(RestClient client, string token, string docPath, string fileName)
        {
            MemoryStream newStream;
            //Create the service request
            var request = new RestRequest($"documents?fileName={fileName}&user=Test&classify=true", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "multipart/form-data");

            //Get the byte array for streaming. The service also supports other mechanisms for upload e.g. Base64
            byte[] data = GetByteArrayFromPath(docPath);
            newStream = new MemoryStream(data);
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
            IRestResponse response = client.Execute(request);
            return response;
        }

        private static byte[] GetByteArrayFromPath(string path)
        {
            byte[] image = (new WebClient()).DownloadData(path);
            return image;
        }
        private static string GetFileName(string input)
        {
            var fileName = Path.GetFileName(input);
            return fileName;
        }

        /// <summary>
        /// Returns the status of a document (uploaded or processed)
        /// </summary>
        /// <param name = "client" ></ param >
        /// < param name="token"></param>
        private static string GetDocumentStatus(RestClient client, string token, string documentGuid = null)
        {
            //File Guid has to be inserted before calling the service directly
            string fileGuid = documentGuid; //TODO: document Guid to be viewed

            if (string.IsNullOrEmpty(fileGuid))
                Console.WriteLine("Document status demo cannot be proceeded. Document Id is required");

            //Create the service request
            var request = new RestRequest($"documents/{fileGuid}/status", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                DocumentPageViewResponse result = JsonConvert.DeserializeObject<DocumentPageViewResponse>(response.Content);
                Console.WriteLine("Document Status Demo Result: " +result);
                Console.WriteLine($"Result: {response.Content}");
                return result.Status;
            }
            else
            {
                var error = response.ErrorMessage;
                Console.WriteLine(error);
                Console.WriteLine(response.Content);
                return error;
            }
        }

        /// <summary>
        /// Returns the details of a document 
        /// </summary>
        /// <param name = "client" ></ param >
        /// < param name="token"></param>
        private static void ViewDocument(RestClient client, string token, string documentGuid = null)
        {
            //File Guid has to be inserted before calling the service directly
            string fileGuid = documentGuid; //TODO: document Guid to be viewed

            if(string.IsNullOrEmpty(fileGuid))
                Console.WriteLine("Document view demo cannot be proceeded. Document Id is required");

            //Create the service request
            var request = new RestRequest($"documents/{fileGuid}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                DocumentPageViewResponse result = JsonConvert.DeserializeObject<DocumentPageViewResponse>(response.Content);
                Console.WriteLine("Document View Demo Result: ");
                Console.WriteLine($"Result: {response.Content}");
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
                Console.WriteLine(response.Content);
            }
        }
    }
}
