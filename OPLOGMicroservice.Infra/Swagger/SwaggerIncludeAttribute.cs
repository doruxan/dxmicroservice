// <copyright file="SwaggerIncludeAttribute.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Infra.Swagger
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SwaggerIncludeAttribute : Attribute { }
}
