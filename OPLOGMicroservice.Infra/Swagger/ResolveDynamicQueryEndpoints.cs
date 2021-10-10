// <copyright file="ResolveDynamicQueryEndpoints.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using DynamicQueryBuilder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace OPLOGMicroservice.Infra.Swagger
{
    [ExcludeFromCodeCoverage]
    public sealed class ResolveDynamicQueryEndpoints : IOperationFilter
    {
        private readonly string _description;
        private readonly string _dqbResolveParam;

        public ResolveDynamicQueryEndpoints(
            string dqbResolveParam = "",
            string description = "DynamicQuery")
        {
            _description = description;
            _dqbResolveParam = dqbResolveParam;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context?.MethodInfo.GetCustomAttribute<DynamicQueryAttribute>() == null)
            {
                return;
            }

            if (operation.Parameters == null)
            {
                return;
            }

            operation.Parameters.Clear();

            var apiSchema = context.SchemaGenerator.GenerateSchema(typeof(string), context.SchemaRepository);
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Query,
                Schema = apiSchema,
                Description = _description,
                Name = _dqbResolveParam,
            });

            var methodParams = context.MethodInfo.GetParameters();

            foreach (var methodParam in methodParams)
            {
                if (methodParam.GetCustomAttribute<SwaggerIncludeAttribute>() == null)
                {
                    continue;
                }

                apiSchema = context.SchemaGenerator.GenerateSchema(methodParam.ParameterType, context.SchemaRepository);
                if (context.ApiDescription.ActionDescriptor.Parameters.Where(p => p.Name == methodParam.Name).Any(p => p.BindingInfo.BindingSource.DisplayName == "Path"))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        In = ParameterLocation.Path,
                        Name = methodParam.Name,
                        Required = true,
                        Schema = apiSchema,
                    });
                }
                else
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        In = ParameterLocation.Query,
                        Name = methodParam.Name,
                        Required = true,
                        Schema = apiSchema,
                    });
                }
            }
        }
    }
}
