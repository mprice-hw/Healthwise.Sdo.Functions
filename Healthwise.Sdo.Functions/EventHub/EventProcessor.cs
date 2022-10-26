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
using Healthwise.Sdo.Functions.Validation;
using Azure;
using Healthwise.Sdo.Events.AzurePipelines;
using System.Reflection;

namespace Healthwise.Sdo.Functions.EventHub
{
    public class EventProcessor
    {
        private readonly IStorageService _storageService;

        public EventProcessor(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [FunctionName("ProcessEvent")]
        public async Task ProcessEvent([EventHubTrigger("mp-sdo-proto-eventhub", Connection = "EventHubConnectionString")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();       

            foreach (EventData eventData in events)
            {
                try
                {
                    //This code block allows us to use the event type sent with at runtime
                    var eventTypeName = JsonConvert.DeserializeObject<EventBase>(eventData.EventBody.ToString()).Type;
                    var eventAssembly = typeof(EventBase).Assembly;
                    var eventType = eventAssembly.GetTypes().Where(t => t.Name == eventTypeName).First();
                    var methodInfo = typeof(EventDataValidationExtentions).GetMethod("GetEventBody");
                    var genericMethodInfo = methodInfo.MakeGenericMethod(eventType);
                    dynamic eventBody = genericMethodInfo.Invoke(null, new[] { eventData });

                    if (eventBody.IsValid)
                    {
                        await _storageService.AddEventAsync(eventData);
                    }

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
