using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeApplication.Models.DTO
{
    public class ImpressionsDTO
    {
        [JsonProperty(PropertyName = "")]
        public List<ImpressionDetailsDTO> ImpressionDetails { get; set; }
    }
}
