using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeApplication.Clients
{
    public interface IAdformClient
    {
        Task<(HttpStatusCode httpStatus, TResp response)> CallClient<TResp>(HttpMethod method,
            string resource, object model = null) where TResp : class;
    }
}
