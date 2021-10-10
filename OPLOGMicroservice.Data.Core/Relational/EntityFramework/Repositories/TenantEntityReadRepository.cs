// <copyright file="TenantReadRepository.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories
{
    public class TenantEntityReadRepository<TEntity> : EntityReadRepository<TEntity>, ITenantEntityReadRepository<TEntity>
        where TEntity : TenantEntity
    {
        public TenantEntityReadRepository(IReadDbContext baseDb)
            : base(baseDb)
        {
        }

        public async Task<bool> DoesTenantEntityExistAsync(Guid tenantId, Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetTenantEntityById(tenantId, id, isIgnoreQueryFilter).AnyAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAllByTenantId(Guid tenantId, bool isIgnoreQueryFilter = false)
        {
            var query = _set.Where(u => u.TenantId == tenantId);

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAllByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetAllByTenantId(tenantId, isIgnoreQueryFilter).ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetTenantEntityById(Guid tenantId, Guid id, bool isIgnoreQueryFilter = false)
        {
            var query = _set.Where(u => u.TenantId == tenantId && u.Id == id);

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<TEntity> GetTenantEntityByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetTenantEntityById(tenantId, id, isIgnoreQueryFilter).FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class TenantEntityReadRepository<TEntity, TReferenceNumber> : TenantEntityReadRepository<TEntity>, ITenantEntityReadRepository<TEntity, TReferenceNumber>
     where TEntity : TenantEntity<TReferenceNumber>
    {
        public TenantEntityReadRepository(IReadDbContext baseDb)
            : base(baseDb)
        {
        }

        public async Task<TEntity> GetTenantEntityByReferenceNumberAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetTenantEntityByReferenceNumber(tenantId, referenceNumber, isIgnoreQueryFilter).FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetTenantEntityByReferenceNumber(Guid tenantId, TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false)
        {
            var query = _set.Where(item => item.ReferenceNumber.Equals(referenceNumber) && item.TenantId.Equals(tenantId));

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<bool> DoesTenantEntityExistAsync(Guid tenantId, TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetTenantEntityByReferenceNumber(tenantId, referenceNumber, isIgnoreQueryFilter).AnyAsync(cancellationToken);
        }
    }
}
