using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts
{
    public interface IDbContext
    {
        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }

        IModel Model { get; }

        void Dispose();

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        EntityEntry Entry([NotNullAttribute] object entity);

        bool Equals(object obj);

        object Find([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);

        TEntity Find<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;

        ValueTask<TEntity> FindAsync<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;

        ValueTask<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity> FindAsync<TEntity>([CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

        ValueTask<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);

        int GetHashCode();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        string ToString();
    }
}
