using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sdo.functions.Events.Jira
{
    internal class JiraProject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
