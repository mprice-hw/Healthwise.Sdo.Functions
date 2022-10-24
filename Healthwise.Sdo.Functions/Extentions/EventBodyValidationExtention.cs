using Azure.Messaging.EventHubs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Extentions
{
    public static class EventBodyValidationExtention
    {
        private static ValidationWrapper<T> BuildValidationWrapper<T>(string eventBodyString)
        {
            ValidationWrapper<T> eventBody = new ValidationWrapper<T>();
            eventBody.Value = JsonConvert.DeserializeObject<T>(eventBodyString);

            var results = new List<ValidationResult>();
            eventBody.IsValid = Validator.TryValidateObject(eventBody.Value, new ValidationContext(eventBody.Value, null, null), results, true);
            eventBody.ValidationResults = results;

            return eventBody;
        }

        public static async Task<ValidationWrapper<T>> GetEventBodyAsync<T>(this EventData eventData)
        {
            var eventBodyString = eventData.EventBody.ToString();
            return BuildValidationWrapper<T>(eventBodyString);
        }
    }
}

