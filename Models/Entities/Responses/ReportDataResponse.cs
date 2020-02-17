using ChallengeApplication.Models.Entities.Responses;
using Newtonsoft.Json;

namespace ChallengeApplication.Models.Entities
{
    public class ReportDataResponse
    {
        [JsonProperty(PropertyName = "reportData")]
        public ReportData ReportData { get; set; }
    }
}
