// <copyright file="IWriteRepository.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories
{
    public interface IEntityWriteRepository<TEntity> : IEntityReadRepository<TEntity>
        where TEntity : Entity
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false);

        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken, bool saveChanges = false);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveChanges = false);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }

    public interface IEntityWriteRepository<TEntity, TReferenceNumber> : IEntityWriteRepository<TEntity>
       where TEntity : Entity<TReferenceNumber>
    {
        Task<TEntity> GetEntityByReferenceNumberAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetEntityByReferenceNumber(TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false);

        Task<bool> DoesEntityExistAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }
}
