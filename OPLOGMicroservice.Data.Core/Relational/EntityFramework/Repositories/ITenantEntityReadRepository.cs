// <copyright file="ITenantReadRepository.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories
{
    public interface ITenantEntityReadRepository<TEntity> : IEntityReadRepository<TEntity>
        where TEntity : TenantEntity
    {
        IQueryable<TEntity> GetAllByTenantId(Guid tenantId, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntityById(Guid tenantId, Guid id, bool isIgnoreQueryFilter = false);

        Task<IEnumerable<TEntity>> GetAllByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        Task<TEntity> GetTenantEntityByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        Task<bool> DoesTenantEntityExistAsync(Guid tenantId, Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }

    public interface ITenantEntityReadRepository<TEntity, TReferenceNumber> : ITenantEntityReadRepository<TEntity>
     where TEntity : TenantEntity<TReferenceNumber>
    {
        Task<TEntity> GetTenantEntityByReferenceNumberAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntityByReferenceNumber(Guid tenantId, TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false);

        Task<bool> DoesTenantEntityExistAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }
}