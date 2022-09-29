using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;

namespace sdo.functions.Events.Jira
{
    internal class JiraIssue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("updated")]
        public string Updated { get; set; }
        
        [JsonProperty("project")]
        public JiraProject Project { get; set; }

        [JsonProperty("status")]
        public JiraStatus Status { get; set; }

        [JsonProperty("issueType")]
        public JiraIssueType IssueType { get; set; }


        public string TimeStamp
        {
            get 
            {
                var dateTime = DateTime.Parse(Updated);
                var timeStamp = dateTime.ToString("yyyyMMddHHmmssfff");
                return timeStamp ;             
            }
        }



    }
}
