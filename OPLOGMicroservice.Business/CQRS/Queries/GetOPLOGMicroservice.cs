using DynamicQueryBuilder.Models;
using MediatR;
using OPLOGMicroservice.Model.OPLOGMicroservice.GetOPLOGMicroservice;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Queries
{
    public class GetOPLOGMicroservice : IRequest<GetOPLOGMicroserviceResponse>
    {
        public DynamicQueryOptions QueryOptions { get; set; }
    }

    public class GetOPLOGMicroserviceHandler: IRequestHandler<GetOPLOGMicroservice, GetOPLOGMicroserviceResponse>
    {
        public Task<GetOPLOGMicroserviceResponse> Handle(GetOPLOGMicroservice request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
