using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sdo.functions.Events.Jira
{
    internal class JiraIssue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        [JsonPropertyName("updated")]
        public string Updated { get; set; }
        
        [JsonPropertyName("project")]
        public JiraProject Project { get; set; }

        [JsonPropertyName("status")]
        public JiraStatus Status { get; set; }

        [JsonPropertyName("issueType")]
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
