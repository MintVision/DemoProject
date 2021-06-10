using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Newtonsoft.Json;

namespace EventHubsReceiver
{
    class Program
    {
        // TODO
        //NB: set your event hub and blob storage details here!!!!!!!!!!!!!
        private const string ehubNamespaceConnectionString = "";
        private const string eventHubName = "";
        private const string blobStorageConnectionString = "";
        private const string blobContainerName = "";
        static async Task Main()
        {
            // TODO
            //NB: set your event hub consumer group here!!!!!!!!!!!!!
            // Read from the consumer group
            string consumerGroup = "";

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            // Stop the processing
            //await processor.StopProcessingAsync();
        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
         {
            var visionEvent = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            // Write the body of the event to the console window
            if (visionEvent.Contains("DocumentUploaded") || visionEvent.Contains("DocumentProcessed") || visionEvent.Contains("PersonRecognised") || visionEvent.Contains("PersonRegistered"))
            {
                Console.WriteLine("Received event: {0}", JsonConvert.DeserializeObject(visionEvent));
            }  
            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"Partition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
