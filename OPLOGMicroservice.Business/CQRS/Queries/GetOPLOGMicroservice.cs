using DynamicQueryBuilder.Models;
using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Model.OPLOGMicroservice.GetOPLOGMicroservice;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Queries
{
    public class GetOPLOGMicroservice : IAsyncQuery<GetOPLOGMicroserviceResponse>
    {
        public DynamicQueryOptions QueryOptions { get; set; }
    }

    public class GetOPLOGMicroserviceHandler : IAsyncQueryExecutor<GetOPLOGMicroservice, GetOPLOGMicroserviceResponse>
    {
        public GetOPLOGMicroserviceResponse Execute(GetOPLOGMicroservice query, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetOPLOGMicroserviceResponse> ExecuteAsync(GetOPLOGMicroservice query, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
