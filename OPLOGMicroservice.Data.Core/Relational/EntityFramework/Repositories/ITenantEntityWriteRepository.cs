// <copyright file="IEntityWriteRepository<Tenant>.cs" company="Oplog">
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
    public interface ITenantEntityWriteRepository<TEntity> : IEntityWriteRepository<TEntity>
        where TEntity : TenantEntity
    {
        IQueryable<TEntity> GetAllByTenantId(Guid tenantId, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntityById(Guid tenantId, Guid id, bool isIgnoreQueryFilter = false);

        Task<IEnumerable<TEntity>> GetAllByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        Task<TEntity> GetTenantEntityByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }

    public interface ITenantEntityWriteRepository<TEntity, TReferenceNumber> : ITenantEntityWriteRepository<TEntity>
     where TEntity : TenantEntity<TReferenceNumber>
    {
        Task<TEntity> GetTenantEntityByReferenceNumberAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntityByReferenceNumber(Guid tenantId, TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntitiesByReferenceNumbers(Guid tenantId, IEnumerable<TReferenceNumber> referenceNumbers, bool isIgnoreQueryFilter = false);

        Task<IEnumerable<TEntity>> GetTenantEntitiesByReferenceNumbersAsync(Guid tenantId, IEnumerable<TReferenceNumber> referenceNumbers, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetTenantEntitiesByIds(Guid tenantId, IEnumerable<Guid> ids, bool isIgnoreQueryFilter = false);

        Task<IEnumerable<TEntity>> GetTenantEntitiesByIdsAsync(Guid tenantId, IEnumerable<Guid> ids, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        Task<bool> DoesTenantEntityExistAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }
}
