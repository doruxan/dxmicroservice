// <copyright file="IReadRepository.cs" company="Oplog">
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
    public interface IEntityReadRepository<TEntity>
        where TEntity : Entity
    {
        IQueryable<TEntity> GetAllEntities(bool isIgnoreQueryFilter = false);

        Task<IEnumerable<TEntity>> GetAllEntitiesAsync(CancellationToken can, bool isIgnoreQueryFilter = false);

        Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetEntityById(Guid id, bool isIgnoreQueryFilter = false);

        Task<bool> DoesEntityExistAsync(Guid id, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }

    public interface IEntityReadRepository<TEntity, TReferenceNumber> : IEntityReadRepository<TEntity>
       where TEntity : Entity<TReferenceNumber>
    {
        Task<TEntity> GetEntityByReferenceNumberAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);

        IQueryable<TEntity> GetEntityByReferenceNumber(TReferenceNumber referenceNumber, bool isIgnoreQueryFilter = false);

        Task<bool> DoesEntityExistAsync(TReferenceNumber referenceNumber, CancellationToken cancellationToken, bool isIgnoreQueryFilter = false);
    }
}