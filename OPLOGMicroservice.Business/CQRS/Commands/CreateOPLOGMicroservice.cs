using MediatR;
using OPLOGMicroservice.Data.Core;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using OPLOGMicroservice.Domain;
using OPLOGMicroservice.Model.OPLOGMicroservice.CreateOPLOGMicroservice;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Commands
{
    public class CreateOPLOGMicroservice : IRequest<CreateOPLOGMicroserviceResponse>
    {
        public CreateOPLOGMicroserviceRequest Request { get; set; }
    }

    public class CreateOPLOGMicroserviceHandler : IRequestHandler<CreateOPLOGMicroservice, CreateOPLOGMicroserviceResponse>
    {
        private readonly IEntityWriteRepository<OPLOGMicroserviceEntity> repository;

        public CreateOPLOGMicroserviceHandler(
            IEntityWriteRepository<OPLOGMicroserviceEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<CreateOPLOGMicroserviceResponse> Handle(CreateOPLOGMicroservice query, CancellationToken cancellationToken)
        {
            var entity = new OPLOGMicroserviceEntity(query.Request.Name);

            await repository.AddAsync(entity, cancellationToken);

            return new CreateOPLOGMicroserviceResponse { Entity = entity };
        }
    }
}
