using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Healthwise.Sdo.Functions.Events.AzurePipelines
{
    internal class PipelineExecutionEvent : EventBase
    {
        [JsonProperty("name")]
        public string PipelineName { get; set; }

        [JsonProperty("runId")]
        public int RunId { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonProperty("serviceVersion")]
        public string ServiceVersion { get; set; }

        [JsonProperty("repoName")]
        public string RepoName { get; set; }

        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        [JsonProperty("commitHash")]
        public string CommitHash { get; set; }

        [JsonProperty("state")]
        public PipelineState State { get; set; }

        [JsonProperty("result")]
        public PipelineResult Result { get; set; }
    }
}
