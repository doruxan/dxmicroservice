using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Infra
{
    public interface IBaseService
    {
        Task CommitAsync(CancellationToken cancellationToken);

        Task<T> SendQueryAsync<T>(IRequest<T> query, CancellationToken cancellationToken);

        Task<T> SendCommandAsync<T>(IRequest<T> command, CancellationToken cancellationToken);
    }
}
