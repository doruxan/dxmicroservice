using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IWriteDbContext DBContext { get; }

        Task RenewTransactionAsync(CancellationToken cancellationToken);

        Task CommitAsync(CancellationToken cancellationToken);

        Task RollbackAsync(CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
