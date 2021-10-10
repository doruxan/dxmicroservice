using OPLOGMicroservice.Data.Core.Relational.EntityFramework;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Data
{
    [ExcludeFromCodeCoverage]
    public sealed class OPLOGMicroserviceUnitOfWork : EfUnitOfWork
    {
        public OPLOGMicroserviceUnitOfWork(OPLOGMicroserviceWriteDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
