using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Healthwise.Sdo.Events;
using Healthwise.Sdo.Functions.Exceptions;

namespace Healthwise.Sdo.Functions
{
    public static class ProcessEvent
    {

        [FunctionName("ProcessEvent")]
        public static async Task Run([EventHubTrigger("mp-sdo-proto-eventhub", Connection = "EventHubConnectionString")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var destinationContainerName = GetDestinationContainerName(eventData);
                    var blobStorageContainerUri = new Uri((Environment.GetEnvironmentVariable("BlobStorageUri") + "/" + destinationContainerName));
                    var storageSharedKeyCred = new StorageSharedKeyCredential("mpsdoprotostorage", Environment.GetEnvironmentVariable("BlobStorageSharedKey"));


                    var blobClient = new BlobContainerClient(blobStorageContainerUri, storageSharedKeyCred);

                    var identifier = GetIdentifier(eventData);
                    var blob = blobClient.GetBlobClient(identifier);
                    await blob.UploadAsync(eventData.EventBody);

                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }

        private static string GetIdentifier(EventData eventData)
        {
            var eventString = eventData.EventBody.ToString();
            var eventObject = JsonConvert.DeserializeObject<EventBase>(eventString);
            var identifier = eventObject.SourceId;
            return identifier;
        }

        private static string GetDestinationContainerName(EventData eventData) 
        {
            var eventString = eventData.EventBody.ToString();
            var eventObject = JsonConvert.DeserializeObject<EventBase>(eventString);

            if (eventObject.Type == "PipelineExecutionEvent")
            {
                return "azure-pipeline-events";
            }
            else if (eventObject.Type == "IssueTransitionEvent")
            {
                return "jira-issue-events";
            }

            throw new InvalidEventTypeException("The EventType for this event is invalid.");            
        }
    }
}
