using Azure.Messaging.EventHubs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Validation
{
    public static class EventDataValidationExtentions
    {
        private static ValidationWrapper<T> BuildValidationWrapper<T>(string eventBodyString)
        {
            ValidationWrapper<T> eventBody = new ValidationWrapper<T>();
            var results = new List<ValidationResult>();

            //Validating Json can be deserialized before validating data annotations. This seems like it could be better.
            string outErrorMessage;
            T outValue;
            var isValidJson = eventBodyString.TryParse<T>(out outValue, out outErrorMessage);
         
            if (!isValidJson)
            {
                eventBody.IsValid = false;
                results.Add(new ValidationResult(outErrorMessage));
                eventBody.ValidationResults = results;
            }
            else
            {
                eventBody.Value = outValue;
                eventBody.IsValid = Validator.TryValidateObject(eventBody.Value, new ValidationContext(eventBody.Value, null, null), results, true);
                eventBody.ValidationResults = results;
            }

            return eventBody;
        }

        public static ValidationWrapper<T> GetEventBody<T>(this EventData eventData)
        {
            var eventBodyString = eventData.EventBody.ToString();
            return BuildValidationWrapper<T>(eventBodyString);
        }
    }
}