using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Auth
{
    public static class JwtBearerEventHandlers
    {
        public static Task OnChallenge(JwtBearerChallengeContext context)
        {
            var exceptionMessage = "Unauthorized";
            var headerValue = context.Options.Challenge;

            // Some of authorization/authentication error default response headers:
            // www -authenticate: Bearer
            // www -authenticate: Bearer error="invalid_token"
            // www -authenticate: Bearer error="invalid_token", error_description="The token is expired"
            if (!string.IsNullOrWhiteSpace(context.Error))
            {
                exceptionMessage = context.Error;
                headerValue = $"{headerValue} error=\"{context.Error}\"";
            }

            if (!string.IsNullOrWhiteSpace(context.ErrorDescription))
            {
                exceptionMessage = context.ErrorDescription;
                headerValue = $"{headerValue}, error_description=\"{context.ErrorDescription}\"";
            }

            context.Response.Headers.TryAdd(HeaderNames.WWWAuthenticate, headerValue);
            throw new UnauthorizedAccessException(exceptionMessage);
        }
    }
}
