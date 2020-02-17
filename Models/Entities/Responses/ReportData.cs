using Newtonsoft.Json;

namespace ChallengeApplication.Models.Entities.Responses
{
    public class ReportData
    {
        [JsonProperty(PropertyName = "rows")]
        public object[,] Rows { get; set; }
    }
}
