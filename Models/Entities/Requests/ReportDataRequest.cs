using ChallengeApplication.Models.Entities.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeApplication.Models.Requests
{
    public class ReportDataRequest
    {
        [JsonProperty(PropertyName = "filter")]
        public Filter Filter { get; set; }

        [JsonProperty(PropertyName = "dimensions")]
        public List<string> Dimensions { get; set; }

        [JsonProperty(PropertyName = "metrics")]
        public List<string> Metrics { get; set; }
    }
}
