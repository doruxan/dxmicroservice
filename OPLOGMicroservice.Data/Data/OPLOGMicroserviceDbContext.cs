using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddCommonQueryFiltersAndConfigurationsToAllModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
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
