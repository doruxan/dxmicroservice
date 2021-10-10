using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core;
using OPLOGMicroservice.Data.Data;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.DbUtils
{
    public interface IEfDbRecreator
    {
        Task RecreateDbWithMigrationScriptsAndSeedAsync(CancellationToken cancellationToken = default);
    }

    public class EfDbRecreator : IEfDbRecreator
    {
        private readonly OPLOGMicroserviceWriteDbContext _dbContext;
        private readonly IUnitOfWork _uow;

        public EfDbRecreator(OPLOGMicroserviceWriteDbContext dbContext, IUnitOfWork uow)
        {
            _dbContext = dbContext;
            _uow = uow;
        }

        public async Task RecreateDbWithMigrationScriptsAndSeedAsync(CancellationToken cancellationToken = default)
        {
            DbConnection dbConnection = _dbContext.Database.GetDbConnection();

            if (dbConnection.DataSource.Equals("oplogmicroservice.database.windows.net", StringComparison.InvariantCultureIgnoreCase) && dbConnection.Database.Equals("oplogmicroservice", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException();
            }

            await DropTablesAsync(cancellationToken).ConfigureAwait(false);

            await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);

            await _uow.RenewTransactionAsync(cancellationToken).ConfigureAwait(false);
            //_dbContext.EnsureSeeded(_uow);
        }

        private async Task DropTablesAsync(CancellationToken cancellationToken = default)
        {
            string sqlCode = @"while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
                                 	begin
                                 	 declare @sql nvarchar(2000)
                                 	 SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
                                 	 FROM information_schema.table_constraints
                                 	 WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
                                     exec (@sql)
                                 	end
                                 
                               while(exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA != 'sys'))
                                 	begin
                                 	 SELECT TOP 1 @sql=('DROP TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME + ']')
                                 	 FROM INFORMATION_SCHEMA.TABLES
                                 	 WHERE TABLE_SCHEMA != 'sys'
                                 	 exec (@sql)
                                 	end";

            await _dbContext.Database.ExecuteSqlRawAsync(sqlCode, cancellationToken).ConfigureAwait(false);
            if (_dbContext.Database.CurrentTransaction != null)
            {
                await _dbContext.Database.CurrentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
