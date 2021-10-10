using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.NonRelational.Mongo
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        public MongoUnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public IWriteDbContext DBContext => throw new NotImplementedException();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChanges();
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task RenewTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
