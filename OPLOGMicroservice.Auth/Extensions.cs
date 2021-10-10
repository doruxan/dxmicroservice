using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace OPLOGMicroservice.Auth
{
    public static class Extensions
    {
        public static void AddOPLOGAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               //options.Authority = config.Auth0.Domain;
               //options.Audience = config.Auth0.ApiIdentifier;
               //options.RequireHttpsMetadata = config.Auth0.RequireHttpsMetadata;
               //options.MetadataAddress = $"{config.Auth0.Domain}/.well-known/openid-configuration";
               options.Events = new JwtBearerEvents
               {
                   OnChallenge = JwtBearerEventHandlers.OnChallenge
               };
           });
        }

        public static void AddOPLOGAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPermissions.TENANT_USER,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.TENANT_USER,
                        "config.Auth0.Domain")));
                options.AddPolicy(AuthorizationPermissions.TENANT_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.TENANT_ADMIN,
                        "config.Auth0.Domain")));
                options.AddPolicy(AuthorizationPermissions.SYSTEM_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.SYSTEM_ADMIN,
                        "config.Auth0.Domain")));
                options.AddPolicy(AuthorizationPermissions.WAREHOUSE_ADMIN,
                    policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPermissions.WAREHOUSE_ADMIN,
                        "config.Auth0.Domain")));
            });
        }
    }
}
