using System;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework
{
    public interface IEfEntity : IRelationalEntity
    {
        public Guid Id { get; set; }
    }
}
