using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OPLOGMicroservice.Auth;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPLOGMicroservice.Infra.Swagger
{
    public static class SwaggerExtensions
    {
        public const string DqbResolveParam = "dqb";

        public static void AddOPLOGSwagger(this IServiceCollection services, string apiVersion, string apiName, Auth0Options auth0Options, IWebHostEnvironment env)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ResolveDynamicQueryEndpoints>(DqbResolveParam);
                c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiName, Version = apiVersion });

                c.CustomSchemaIds(DefaultSchemaIdSelector);
                c.SchemaFilter<RequireValueTypePropertiesSchemaFilter>(true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    OpenIdConnectUrl = new Uri($"{auth0Options.Domain}/.well-known/openid-configuration",
                        UriKind.Absolute),
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl =
                                new Uri(
                                    $"{auth0Options.ClientAuthorizationUrl}/authorize?audience={auth0Options.ClientAudience}",
                                    UriKind.Absolute),
                            Scopes = new Dictionary<string, string>(),
                        },
                    },
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.OAuth2,
                            OpenIdConnectUrl = new Uri($"{auth0Options.Domain}/.well-known/openid-configuration",
                                UriKind.Absolute),
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl =
                                        new Uri(
                                            $"{auth0Options.ClientAuthorizationUrl}/authorize?audience={auth0Options.ClientAudience}",
                                            UriKind.Absolute),
                                    Scopes = new Dictionary<string, string>(),
                                },
                            },
                        },
                        new List<string>()
                    },
                });

                List<string> enabledAreas = new List<string> { "openapi" };

                if (!env.IsProduction() && !env.IsStaging())
                {
                    enabledAreas.Add("api");
                    enabledAreas.Add("admin");
                    enabledAreas.Add("dev");
                    enabledAreas.Add("warehouse");
                    enabledAreas.Add("mobile");
                    enabledAreas.Add("maestro");

                    if (env.IsStaging())
                    {
                        c.OperationFilter<TenantIdOperationFilter>();
                    }
                }

                c.DocumentFilter<SwaggerAreaFilter>(new object[] { enabledAreas.ToArray() });
                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });
        }

        public static void UseOPLOGSwaggerUI(this IApplicationBuilder app, string apiTitle, Auth0Options auth0Options)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiTitle);
                c.DocExpansion(DocExpansion.None);
                c.OAuthClientId(auth0Options.SPAClientId);
                c.DisplayRequestDuration();
            });
        }


        private static string DefaultSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType)
            {
                return modelType.Name;
            }

            var prefix = modelType.GetGenericArguments()
                .Select(DefaultSchemaIdSelector)
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }
    }
}
