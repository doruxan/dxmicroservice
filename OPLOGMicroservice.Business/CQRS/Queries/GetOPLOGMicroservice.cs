using DynamicQueryBuilder.Models;
using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Model.OPLOGMicroservice.GetOPLOGMicroservice;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Queries
{
    public class GetOPLOGMicroservice<TDto> : IAsyncQuery<GetOPLOGMicroserviceResponse>
    {
        public DynamicQueryOptions QueryOptions { get; set; }
    }

    public class GetOPLOGMicroserviceHandler<T> : IAsyncQueryExecutor<GetOPLOGMicroservice<T>, GetOPLOGMicroserviceResponse>
    {
        public GetOPLOGMicroserviceResponse Execute(GetOPLOGMicroservice<T> query, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetOPLOGMicroserviceResponse> ExecuteAsync(GetOPLOGMicroservice<T> query, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
