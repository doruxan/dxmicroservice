// <copyright file="BaseWriteRepository.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories
{
    public class EntityWriteRepository<TEntity> : IEntityWriteRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly IUnitOfWork _uow;
        protected readonly DbSet<TEntity> _set;

        public EntityWriteRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _set = _uow.DBContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllEntities(bool isIgnoreQueryFilter = false)
        {
            return _set;
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
            return await GetAllEntities().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false)
        {
            await _set.AddAsync(entity, cancellationToken);
            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false)
        {
            await _set.AddRangeAsync(entities, cancellationToken);
            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false)
        {
            _set.Remove(entity);
            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false)
        {
            _set.RemoveRange(entities);
            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false)
        {
            foreach (TEntity entity in entities)
            {
                entity.Update();
            }
            _set.UpdateRange(entities);

            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false)
        {
            entity.Update();
            _set.Update(entity);
            if (saveChanges)
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _uow.DBContext.ChangeTracker.DetectChanges();
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DoesEntityExistAsync(Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false)
        {
            return await GetEntityById(id, isIgnoreQueryFilter).AnyAsync(cancellationToken);
        }
    }

    public class EntityWriteRepository<TEntity, TReferenceNumber> : EntityWriteRepository<TEntity>, IEntityWriteRepository<TEntity, TReferenceNumber>
       where TEntity : Entity<TReferenceNumber>
    {
        public EntityWriteRepository(IUnitOfWork uow)
            : base(uow)
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
