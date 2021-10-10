using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Data
{
    [ExcludeFromCodeCoverage]
    public class OPLOGMicroserviceWriteDbContext : OPLOGMicroserviceDbContext, IWriteDbContext
    {
        public OPLOGMicroserviceWriteDbContext(DbContextOptions<OPLOGMicroserviceWriteDbContext> options)
          : base(options)
        {
        }
    }
}
