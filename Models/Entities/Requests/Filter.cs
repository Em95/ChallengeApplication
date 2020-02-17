using Newtonsoft.Json;

namespace ChallengeApplication.Models.Entities.Requests
{
    public class Filter
    {
        // Filter properties declared by task usage
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
    }
}
