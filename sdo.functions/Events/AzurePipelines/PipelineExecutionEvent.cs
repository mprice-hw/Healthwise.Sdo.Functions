using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sdo.functions.Events.AzurePipelines
{
    internal class PipelineExecutionEvent : EventBase
    {
        [JsonPropertyName("name")]
        public string PipelineName { get; set; }

        [JsonPropertyName("runId")]
        public int RunId { get; set; }

        [JsonPropertyName("teamName")]
        public string TeamName { get; set; }

        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }

        [JsonPropertyName("serviceVersion")]
        public string ServiceVersion { get; set; }

        [JsonPropertyName("repoName")]
        public string RepoName { get; set; }

        [JsonPropertyName("branchName")]
        public string BranchName { get; set; }

        [JsonPropertyName("commitHash")]
        public string CommitHash { get; set; }

        [JsonPropertyName("status")]
        public PipelineStatus Status { get; set; }
    }
}
