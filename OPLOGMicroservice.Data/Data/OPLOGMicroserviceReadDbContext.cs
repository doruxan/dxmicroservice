using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Data
{
    //Virtual member call in constructor
    //Sealed for more safely
    //https://docs.microsoft.com/en-us/archive/blogs/ericlippert/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one
    //https://www.jetbrains.com/help/resharper/2021.2/VirtualMemberCallInConstructor.html
    [ExcludeFromCodeCoverage]
    public class OPLOGMicroserviceReadDbContext : OPLOGMicroserviceDbContext, IReadDbContext
    {
        public OPLOGMicroserviceReadDbContext(DbContextOptions<OPLOGMicroserviceReadDbContext> options)
         : base(options)
        {
            if (Database.CurrentTransaction == null)
            {
                Database.BeginTransaction(IsolationLevel.Snapshot);
            }
        }
    }
}
