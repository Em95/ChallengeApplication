using ChallengeApplication.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ChallengeApplication.Middleware
{
    public class OAuthAccessHandler
    {
        private readonly RequestDelegate _next;
        private readonly IOAuthClient _oAuthClient;
        public OAuthAccessHandler(RequestDelegate next, IOAuthClient oAuthClient)
        {
            _next = next;
            _oAuthClient = oAuthClient;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tokenDetails = await _oAuthClient.GetTokenDetailsAsync();
            if (tokenDetails != null)
            {
                httpContext.Request.Headers["token-type"] = tokenDetails.Token_type;
                httpContext.Request.Headers["token"] = tokenDetails.Access_token;
                await _next(httpContext);
            }
            else throw new UnauthorizedAccessException();
        }
    }
       
        public static class OAuthAccessHandlerExtensions
        {
            public static IApplicationBuilder UseOAuthAccesHandler(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<OAuthAccessHandler>();
            }
        }

}
