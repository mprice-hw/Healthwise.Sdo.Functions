using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Healthwise.Sdo.Events;
using Healthwise.Sdo.Functions.Exceptions;
using Newtonsoft.Json;

namespace Healthwise.Sdo.Functions.Services
{
    public class AzureBlobStorageService : IStorageService
    {
        public async Task AddEventAsync(EventData eventData)
        {
            var destinationContainerName = GetDestinationContainerName(eventData);
            var blobStorageContainerUri = new Uri((Environment.GetEnvironmentVariable("BlobStorageUri") + "/" + destinationContainerName));
            var storageSharedKeyCred = new StorageSharedKeyCredential("mpsdoprotostorage", Environment.GetEnvironmentVariable("BlobStorageSharedKey"));


            var blobClient = new BlobContainerClient(blobStorageContainerUri, storageSharedKeyCred);

            var identifier = GetIdentifier(eventData);
            var blob = blobClient.GetBlobClient(identifier);
            await blob.UploadAsync(eventData.EventBody);
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
