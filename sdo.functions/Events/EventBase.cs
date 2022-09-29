using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;

namespace sdo.functions.Events
{
    internal class EventBase
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("timeStamp")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset TimeStamp { get; set; }
    }
}
