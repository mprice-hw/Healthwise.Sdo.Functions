using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Healthwise.Sdo.Events;
using Healthwise.Sdo.Functions.Exceptions;
using Healthwise.Sdo.Functions.Services;
using System.ComponentModel.DataAnnotations;

namespace Healthwise.Sdo.Functions.EventHub
{
    public class ProcessEvent
    {
        private readonly IStorageService _storageService;

        public ProcessEvent(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [FunctionName("ProcessEvent")]
        public async Task Run([EventHubTrigger("mp-sdo-proto-eventhub", Connection = "EventHubConnectionString")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();       

            foreach (EventData eventData in events)
            {
                try
                {
                    await _storageService.AddEventAsync(eventData);
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
    }
}
