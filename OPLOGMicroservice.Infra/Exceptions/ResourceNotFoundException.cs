// <copyright file="ResourceNotFoundException.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Infra.Exceptions
{
    [ExcludeFromCodeCoverage]
#pragma warning disable CA2237 // Mark ISerializable types with serializable
    public class ResourceNotFoundException : ValidationException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
    {
        public ResourceNotFoundException(string name, object key)
        : base(ModifyErrors(name, key))
        {
        }

        public ResourceNotFoundException(string entity, string name, object key)
         : base(ModifyErrors(entity, name, key))
        {
        }

        private static List<ValidationFailure> ModifyErrors(string entity, string propName, object key)
        {
            var failure = new ValidationFailure(propName, $"Entity \"{entity}\" with {propName}: {key} was not found.")
            {
                ErrorCode = ErrorCode.ResourceNotFound.ToString()
            };
            return new List<ValidationFailure> { failure };
        }

        private static List<ValidationFailure> ModifyErrors(string propName, object key)
        {
            var failure = new ValidationFailure(propName, $"{propName}: {key} was not found.")
            {
                ErrorCode = ErrorCode.ResourceNotFound.ToString()
            };
            return new List<ValidationFailure> { failure };
        }
    }
}