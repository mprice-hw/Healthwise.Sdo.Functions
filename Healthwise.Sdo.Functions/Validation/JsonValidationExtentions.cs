using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Validation
{
    public static class JsonValidationExtentions
    {
        public static bool TryParse<T>(this string obj, out T result, out string errorMessage)
        {
            try
            {
                // Validate missing fields of object
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MissingMemberHandling = MissingMemberHandling.Error;

                result = JsonConvert.DeserializeObject<T>(obj, settings);
                errorMessage = null;
                return true;
            }
            catch (Exception e)
            {
                result = default(T);
                errorMessage = e.Message;
                return false;
            }
        }
    }
}
