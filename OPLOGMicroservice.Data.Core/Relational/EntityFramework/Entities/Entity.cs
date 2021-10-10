// <copyright file="Entity.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        protected Entity() { }

        public Entity(Guid id)
        {
            var utcNow = DateTime.UtcNow;
            CreatedAt = utcNow;
            UpdatedAt = utcNow;
            Id = id;
        }

        [Key]
        public Guid Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }

        public virtual void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class Entity<T> : Entity
    {
        protected Entity() { }

        public Entity(Guid id)
            : base(id)
        {
        }

        public Entity(Guid id, T referenceNumber)
          : base(id)
        {
            ReferenceNumber = referenceNumber;
        }

        public T ReferenceNumber { get; private set; }
    }
}
