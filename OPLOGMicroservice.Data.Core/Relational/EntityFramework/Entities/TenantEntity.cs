// <copyright file="TenantEntity.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class TenantEntity : Entity
    {
        public Guid TenantId { get; protected set; }

        protected TenantEntity() { }

        public TenantEntity(Guid id, Guid tenantId)
                : base(id)
        {
            TenantId = tenantId;
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class TenantEntity<T> : TenantEntity
    {
        protected TenantEntity() { }

        public TenantEntity(Guid id, Guid tenantId)
                : base(id, tenantId)
        {
        }

        public TenantEntity(Guid id, Guid tenantId, T referenceNumber)
        : base(id, tenantId)
        {
            ReferenceNumber = referenceNumber;
        }

        public T ReferenceNumber { get; private set; }
    }
}
