using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChallengeApplication.Clients
{
    public class AdformClient :IAdformClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;

        public AdformClient(HttpClient client, IHttpContextAccessor httpContext)
        {
            _httpClient = client;
            _httpContext = httpContext;
        }

        public async Task<(HttpStatusCode httpStatus, TResp response)> CallClient<TResp>(HttpMethod method,
            string resource, object model = null) where TResp : class
        {
            var request = new HttpRequestMessage(method, resource);
          
            if (method != HttpMethod.Get && model != null)
                request.Content = new JsonContent(model, "application/json");

            var tokenType = _httpContext.HttpContext.Request.Headers["token-type"].ToString();
            var token = _httpContext.HttpContext.Request.Headers["token"].ToString();
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenType, token);

            TResp responseBody = null;

            var result = await _httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode && result.StatusCode != HttpStatusCode.NoContent)
                responseBody = JsonConvert.DeserializeObject<TResp>(await result.Content.ReadAsStringAsync());

            return (result.StatusCode, responseBody);

        }
    }

}