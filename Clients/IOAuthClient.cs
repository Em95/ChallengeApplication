using ChallengeApplication.Models;
using System.Threading.Tasks;

namespace ChallengeApplication.Clients
{
    public interface IOAuthClient
    {
       Task<TokenDetails> GetTokenDetailsAsync();
    }
}
