using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts
{
    public interface IWriteDbContext : IReadDbContext
    {
        EntityEntry Add([NotNullAttribute] object entity);

        EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        ValueTask<EntityEntry> AddAsync([NotNullAttribute] object entity, CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNullAttribute] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        void AddRange([NotNullAttribute] IEnumerable<object> entities);

        void AddRange([NotNullAttribute] params object[] entities);

        Task AddRangeAsync([NotNullAttribute] IEnumerable<object> entities, CancellationToken cancellationToken = default);

        Task AddRangeAsync([NotNullAttribute] params object[] entities);

        EntityEntry<TEntity> Attach<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        EntityEntry Attach([NotNullAttribute] object entity);

        void AttachRange([NotNullAttribute] params object[] entities);

        void AttachRange([NotNullAttribute] IEnumerable<object> entities);

        EntityEntry Remove([NotNullAttribute] object entity);

        EntityEntry<TEntity> Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        void RemoveRange([NotNullAttribute] IEnumerable<object> entities);

        void RemoveRange([NotNullAttribute] params object[] entities);

        int SaveChanges(bool acceptAllChangesOnSuccess);

        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Update([NotNullAttribute] object entity);

        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        void UpdateRange([NotNullAttribute] params object[] entities);

        void UpdateRange([NotNullAttribute] IEnumerable<object> entities);
    }
}
