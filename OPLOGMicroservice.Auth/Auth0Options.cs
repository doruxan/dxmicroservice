using System.Collections.Generic;

namespace OPLOGMicroservice.Auth
{
    public class Auth0Options
    {
        public string Domain { get; set; }

        public string ApiIdentifier { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public string ApiV2Base { get; set; }

        public List<string> Callbacks { get; set; }

        public List<string> AllowedOrigins { get; set; }

        public List<string> AllowedLogoutUrls { get; set; }

        public List<string> M2MAppGrantTypes { get; set; }

        public int M2MAppTokenLifeTime { get; set; }

        public string ManagementApiGrantType { get; set; }

        public string ManagementApiClientId { get; set; }

        public string ManagementApiClientSecret { get; set; }

        public string AuthorizationApiUrl { get; set; }

        public string AuthorizationTokenAudience { get; set; }

        public string AuthorizationClientId { get; set; }

        public string AuthorizationClientSecret { get; set; }

        public string DbConnectionName { get; set; }

        public string ClientAuthorizationUrl { get; set; }

        public string ClientAudience { get; set; }

        public string SPAClientId { get; set; }

        public string SharedClientId { get; set; }

        public string SharedClientSecret { get; set; }
    }
}
