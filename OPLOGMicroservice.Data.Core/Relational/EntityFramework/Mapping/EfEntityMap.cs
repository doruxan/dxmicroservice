using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Mapping
{
    public class EfEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        private bool baseInvoked;
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!baseInvoked) throw new Exception("All the base configure methods should be invoked.");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Id);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.DeletedAt);
        }
    }
}
