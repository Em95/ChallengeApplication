using ChallengeApplication.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeApplication.Clients
{
    public class OAuthClient : IOAuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;

        public OAuthClient(HttpClient client, IHttpContextAccessor httpContext)
        {
            _httpClient = client;
            _httpContext = httpContext;
        }

        public async Task<TokenDetails> GetTokenDetailsAsync()
        {
            //These values should be selected from appsettings.json
            var formDictionary = new Dictionary<string, string>();
            formDictionary.Add("grant_type", "client_credentials");
            formDictionary.Add("scope", "https://api.adform.com/scope/eapi");
            formDictionary.Add("client_id", "publisherstats.public@adform.com");
            formDictionary.Add("client_secret", "oR43Pjn0q03DvQUitrIJK4092TyrdjXmLKtPAwDb");
            var request = new FormUrlEncodedContent(formDictionary);

            var result = await _httpClient.PostAsync("https://id.adform.com/sts/connect/token", request);
            return result.IsSuccessStatusCode && result.StatusCode != HttpStatusCode.NoContent
                ? JsonConvert.DeserializeObject<TokenDetails>(await result.Content.ReadAsStringAsync())
                : null;
        }
    }

}