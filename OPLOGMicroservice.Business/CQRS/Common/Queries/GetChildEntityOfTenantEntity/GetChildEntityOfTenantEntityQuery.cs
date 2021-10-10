using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetChildEntityOfTenantEntity
{
    public class GetChildEntityOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequest<TDto>
        where TEntity : TenantEntity<TReferenceNumber>
        where TDto : class
    {
        public GetChildEntityOfTenantEntityQuery(Guid tenantId, TReferenceNumber referenceNumber, Expression<Func<TEntity, TChildEntity>> selector)
        {
            TenantId = tenantId;
            ReferenceNumber = referenceNumber;
            Selector = selector;
            new GetChildEntityOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        public TReferenceNumber ReferenceNumber { get; set; }

        public Expression<Func<TEntity, TChildEntity>> Selector { get; set; }
    }

    public class GetChildEntityOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto> : AbstractValidator<GetChildEntityOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>>
         where TEntity : TenantEntity<TReferenceNumber>
         where TDto : class
    {
        public GetChildEntityOfTenantEntityQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.ReferenceNumber).NotEmpty();
            RuleFor(x => x.Selector).NotEmpty();
        }
    }

    public class GetChildEntityOfTenantEntityQueryHandler<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequestHandler<GetChildEntityOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>, TDto>
        where TEntity : TenantEntity<TReferenceNumber>
        where TDto : class
    {
        private readonly ITenantEntityReadRepository<TEntity, TReferenceNumber> _tenantEntityReadRepository;

        public GetChildEntityOfTenantEntityQueryHandler(ITenantEntityReadRepository<TEntity, TReferenceNumber> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<TDto> Handle(GetChildEntityOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> query, CancellationToken cancellationToken)
        {
            return await this._tenantEntityReadRepository
                              .GetTenantEntityByReferenceNumber(query.TenantId, query.ReferenceNumber)
                              .Select(query.Selector)
                              .DefaultIfEmpty()
                              .ProjectToType<TDto>()
                              .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
