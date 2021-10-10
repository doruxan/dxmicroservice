// <copyright file="BaseReadRepository.cs" company="Oplog">
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
    public class EntityReadRepository<TEntity> : IEntityReadRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly IReadDbContext _baseDb;

        protected readonly IQueryable<TEntity> _set;

        public EntityReadRepository(IReadDbContext baseDb)
        {
            _baseDb = baseDb;
            _set = _baseDb.Set<TEntity>().AsNoTracking(); // Readonly entity
        }

        public IQueryable<TEntity> GetAllEntities(bool isIgnoreQueryFilter = false)
        {
            var query = _set;

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetEntityById(id, isIgnoreQueryFilter).FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetEntityById(Guid id, bool isIgnoreQueryFilter = false)
        {
            var query = _set.Where(item => item.Id.Equals(id));

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetAllEntities(isIgnoreQueryFilter).ToListAsync(cancellationToken);
        }

        public async Task<bool> DoesEntityExistAsync(Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetEntityById(id, isIgnoreQueryFilter).AnyAsync(cancellationToken);
        }
    }

    public class EntityReadRepository<TEntity, TReferenceNumber> : EntityReadRepository<TEntity>, IEntityReadRepository<TEntity, TReferenceNumber>
       where TEntity : Entity<TReferenceNumber>
    {
        public EntityReadRepository(IReadDbContext baseDb)
            : base(baseDb)
        {
        }

        public async Task<TEntity> GetEntityByReferenceNumberAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetEntityByReferenceNumber(referenceNumber, isIgnoreQueryFilter).FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetEntityByReferenceNumber(TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false)
        {
            var query = _set.Where(item => item.ReferenceNumber.Equals(referenceNumber));

            if (isIgnoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<bool> DoesEntityExistAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetEntityByReferenceNumber(referenceNumber, isIgnoreQueryFilter).AnyAsync(cancellationToken);
        }
    }
}
