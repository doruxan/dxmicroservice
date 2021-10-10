using MediatR;
using OPLOGMicroservice.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Infra
{
    public class BaseService : IBaseService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public BaseService(IUnitOfWork uow, IMediator mediator)
        {
            this._uow = uow;
            this._mediator = mediator;
        }

        public async Task<T> SendQueryAsync<T>(IRequest<T> query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _uow.CommitAsync(cancellationToken);
            }
            catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<T> SendCommandAsync<T>(IRequest<T> command, CancellationToken cancellationToken)
        {
            try
            {
                T result = await _mediator.Send(command, cancellationToken);
                await _uow.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
