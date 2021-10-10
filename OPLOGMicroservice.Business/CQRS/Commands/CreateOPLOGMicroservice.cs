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
        public bool Result { get; set; }
        public CreateOPLOGMicroserviceResponse ReturnValue { get; set; }
    }

    public class CreateOPLOGMicroserviceHandler : IRequestHandler<CreateOPLOGMicroservice, CreateOPLOGMicroserviceResponse>
    {
        private readonly IEntityWriteRepository<OPLOGMicroserviceEntity> repository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOPLOGMicroserviceHandler(
            IEntityWriteRepository<OPLOGMicroserviceEntity> repository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateOPLOGMicroserviceResponse> Handle(CreateOPLOGMicroservice request, CancellationToken cancellationToken)
        {
            //var entity = mapper.Map<ContactEntity>(command.Request);
            //await repository.AddAsync(entity);

            //await unitOfWork.CommitAsync(cancellationToken);

            //command.ReturnValue = mapper.Map<CreateOPLOGMicroserviceResponse>(entity);

            return null;
        }
    }
}
