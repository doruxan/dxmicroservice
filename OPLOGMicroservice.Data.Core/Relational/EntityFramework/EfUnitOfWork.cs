using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public IWriteDbContext DBContext { get; private set; }

        private IDbContextTransaction _transaction;

        private const IsolationLevel ISOLATIONLEVEL = IsolationLevel.ReadCommitted;

        public EfUnitOfWork(IWriteDbContext dbContext)
        {
            DBContext = dbContext;
            _transaction = DBContext.Database.CurrentTransaction ?? DBContext.Database.BeginTransaction(ISOLATIONLEVEL);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            DBContext.ChangeTracker.DetectChanges();
            await SaveChangesAsync(cancellationToken);
            _transaction.Commit();
            await RenewTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            DBContext.Dispose();
            _transaction.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken)
        {
            _transaction.Rollback();
            await RenewTransactionAsync(cancellationToken);
        }

        public async Task RenewTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction = await DBContext.Database.BeginTransactionAsync(ISOLATIONLEVEL, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await DBContext.SaveChangesAsync(cancellationToken);
        }
    }
}
