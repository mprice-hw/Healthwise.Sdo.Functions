using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using sdo.functions.Events.Jira;

namespace sdo.functions.Events.Jira
{
    internal class JiraStatus
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("statusCategory")]
        public JiraStatusCategory StatusCategory { get; set; }
    }
}
