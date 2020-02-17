using Newtonsoft.Json;
using System.Net.Http;

namespace ChallengeApplication.Clients
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj, string mediaType) :
            base(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, mediaType) { }
    }
}
