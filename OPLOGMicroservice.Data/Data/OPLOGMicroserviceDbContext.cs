using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace OPLOGMicroservice.Data.Data
{
    [ExcludeFromCodeCoverage]
    public class OPLOGMicroserviceDbContext : DbContext
    {
        private readonly string _decimalType = "decimal(19,4)";

        public OPLOGMicroserviceDbContext(DbContextOptions<OPLOGMicroserviceReadDbContext> options)
            : base(options)
        {
            InitializeDbContext();
        }

        public OPLOGMicroserviceDbContext(DbContextOptions<OPLOGMicroserviceWriteDbContext> options)
            : base(options)
        {
            InitializeDbContext();
        }

        public DbSet<OPLOGMicroserviceEntity> OPLOGMicroservices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddCommonQueryFiltersAndConfigurationsToAllModels(modelBuilder);

            var entityTypes = GetEntityTypes(modelBuilder).ToList();
            AddDeletedAtIsNullFilterToNonClusteredIndexes(modelBuilder, entityTypes);
            base.OnModelCreating(modelBuilder);
        }

        private IEnumerable<IMutableEntityType> GetEntityTypes(ModelBuilder builder)
        {
            return builder.Model.GetEntityTypes().Where(x => x.BaseType == null && typeof(Entity).IsAssignableFrom(x.ClrType));
        }

        private void AddDeletedAtIsNullFilterToNonClusteredIndexes(ModelBuilder builder, IEnumerable<IMutableEntityType> entityTypes)
        {
            string deletedAtIndexFilter = $"[{nameof(Entity<Guid>.DeletedAt)}] IS NULL";
            string deletedAtNotNullIndexFilter = $"[{nameof(Entity<Guid>.DeletedAt)}] IS NOT NULL";

            foreach (var entityType in entityTypes)
            {
                var entityClrType = entityType.ClrType;
                var entityTypeBuilder = builder.Entity(entityClrType);

                var nonClusteredUniqueIndexes = entityTypeBuilder.Metadata.GetIndexes().Where(i =>
                    i.IsUnique &&
                    (
                        !i.IsClustered().HasValue ||
                            (i.IsClustered().HasValue && !i.IsClustered().Value)
                    )).ToList();

                foreach (var index in nonClusteredUniqueIndexes)
                {
                    if (!index.Properties.Any(x => x.Name == nameof(Entity<Guid>.DeletedAt)))
                    {
                        var currentIndexes = index.Properties.Select(x => x.Name).ToList();
                        currentIndexes.Add(nameof(Entity<Guid>.DeletedAt));

                        entityTypeBuilder
                            .HasIndex(currentIndexes.ToArray())
                            .IsUnique()
                            .IsClustered(false)
                            .HasFilter(deletedAtIndexFilter);

                        entityTypeBuilder.Metadata.RemoveIndex(index);
                    }
                    else
                    {
                        var indexFilter = index.GetFilter();
                        if (!string.IsNullOrEmpty(indexFilter) && indexFilter.Contains(deletedAtNotNullIndexFilter))
                        {
                            indexFilter = indexFilter.Replace(deletedAtNotNullIndexFilter, deletedAtIndexFilter);
                        }
                        else
                        {
                            indexFilter = deletedAtIndexFilter;
                        }

                        index.SetFilter(indexFilter);
                    }
                }
            }
        }

        private void InitializeDbContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            Database.AutoTransactionsEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        private void AddCommonQueryFiltersAndConfigurationsToAllModels(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes().Where(x => x.BaseType == null && typeof(Entity).IsAssignableFrom(x.ClrType));

            foreach (var entityType in entityTypes)
            {
                var entityClrType = entityType.ClrType;
                var parameter = Expression.Parameter(entityClrType, "p");
                var deletedAtProperty = entityClrType.GetProperty(nameof(Entity.DeletedAt));

                var deletedAtNullExpression = Expression.Equal(Expression.Property(parameter, deletedAtProperty), Expression.Constant(null, typeof(DateTime?)));

                var filter = Expression.Lambda(deletedAtNullExpression, parameter);

                builder.Entity(entityClrType).HasQueryFilter(filter);
                if (!entityClrType.Name.Contains("WayBill"))
                {
                    builder.Entity(entityClrType).Property<Guid>(nameof(Entity.Id)).ValueGeneratedNever();
                }
            }
        }

    }
}
