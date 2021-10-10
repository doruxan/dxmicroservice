using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OPLOGMicroservice.Auth
{
    public static class Extensions
    {
        public static void AddOPLOGAuthentication(this IServiceCollection services, Auth0Options auth0Options)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.Authority = auth0Options.Domain;
               options.Audience = auth0Options.ApiIdentifier;
               options.RequireHttpsMetadata = auth0Options.RequireHttpsMetadata;
               options.MetadataAddress = $"{auth0Options.Domain}/.well-known/openid-configuration";
               options.Events = new JwtBearerEvents
               {
                   OnChallenge = JwtBearerEventHandlers.OnChallenge
               };
           });
        }

        public static void AddOPLOGAuthorization(this IServiceCollection services, Auth0Options auth0Options)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPermissions.TENANT_USER,
                   policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.TENANT_USER,
                       auth0Options.Domain)));
                options.AddPolicy(AuthorizationPermissions.TENANT_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.TENANT_ADMIN,
                        auth0Options.Domain)));
                options.AddPolicy(AuthorizationPermissions.SYSTEM_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.SYSTEM_ADMIN,
                        auth0Options.Domain)));
                options.AddPolicy(AuthorizationPermissions.WAREHOUSE_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.WAREHOUSE_ADMIN,
                        auth0Options.Domain, BasePolicyBehavior.Or)));
            });
        }

        public static void AddOPLOGApiKeyAuth(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await ApiKeyAuthenticationHandler.HandleAuthenticateAsync(context, next);
            });
        }
    }
}
