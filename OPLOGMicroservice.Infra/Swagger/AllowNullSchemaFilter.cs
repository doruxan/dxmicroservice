// <copyright file="AllowNullSchemaFilter.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace OPLOGMicroservice.Infra.Swagger
{
    public class SwaggerNullPropertyAttribute : Attribute { }

    [ExcludeFromCodeCoverage]
    public class AllowNullSchemaFilter : ISchemaFilter
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (model.Properties != null)
            {
                foreach (var item in model.Properties)
                {
                    PropertyInfo objectProperty = context
                        .GetType()
                        .GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (objectProperty != null)
                    {
                        if (objectProperty
                            .CustomAttributes
                            .FirstOrDefault(x => x.AttributeType == typeof(SwaggerNullPropertyAttribute)) == null)
                        {
                            model.Required = model.Required ?? new HashSet<string>();
                            model.Required.Add(item.Key);
                        }
                    }
                }
            }
        }
#pragma warning restore CA1062 // Validate arguments of public methods
    }
}
