using FluentValidation;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllChildEntitiesOfTenantEntity
{
    public class GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequest<IEnumerable<TDto>>
        where TEntity : TenantEntity<TReferenceNumber>
    {
        public GetAllChildEntitiesOfTenantEntityQuery(Guid tenantId, TReferenceNumber referenceNumber, Expression<Func<TEntity, IEnumerable<TChildEntity>>> selector)
        {
            TenantId = tenantId;
            ReferenceNumber = referenceNumber;
            Selector = selector;
            new GetAllChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        public TReferenceNumber ReferenceNumber { get; set; }

        public Expression<Func<TEntity, IEnumerable<TChildEntity>>> Selector { get; set; }
    }

    public class GetAllChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto> : AbstractValidator<GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>>
         where TEntity : TenantEntity<TReferenceNumber>
    {
        public GetAllChildEntitiesOfTenantEntityQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.ReferenceNumber).NotEmpty();
            RuleFor(x => x.Selector).NotEmpty();
        }
    }

    public class GetAllChildEntitiesOfTenantEntityQueryHandler<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequestHandler<GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>, IEnumerable<TDto>>
    where TEntity : TenantEntity<TReferenceNumber>
    {
        private readonly ITenantEntityReadRepository<TEntity, TReferenceNumber> _tenantEntityReadRepository;

        public GetAllChildEntitiesOfTenantEntityQueryHandler(ITenantEntityReadRepository<TEntity, TReferenceNumber> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<IEnumerable<TDto>> Handle(GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> query, CancellationToken cancellationToken)
        {
            return await this._tenantEntityReadRepository
                            .GetTenantEntityByReferenceNumber(query.TenantId, query.ReferenceNumber)
                            .SelectMany(query.Selector)
                            .ProjectToType<TDto>()
                            .ToListAsync(cancellationToken);
        }
    }
}
