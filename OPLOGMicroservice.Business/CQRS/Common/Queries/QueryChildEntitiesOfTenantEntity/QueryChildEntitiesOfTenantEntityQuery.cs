using DynamicQueryBuilder;
using DynamicQueryBuilder.Models;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Business.CQRS.Common.DTOs;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.QueryChildEntitiesOfTenantEntity
{
    public class QueryChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequest<DynamicQueryOutputDTO<TDto>>
        where TEntity : TenantEntity<TReferenceNumber>
    {
        public QueryChildEntitiesOfTenantEntityQuery(Guid tenantId, DynamicQueryOptions queryOptions, TReferenceNumber referenceNumber, Expression<Func<TEntity, IEnumerable<TChildEntity>>> selector)
        {
            TenantId = tenantId;
            QueryOptions = queryOptions;
            ReferenceNumber = referenceNumber;
            Selector = selector;
            new QueryChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        public DynamicQueryOptions QueryOptions { get; set; }

        public TReferenceNumber ReferenceNumber { get; set; }

        public Expression<Func<TEntity, IEnumerable<TChildEntity>>> Selector { get; set; }
    }

    public class QueryChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto> : AbstractValidator<QueryChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>>
         where TEntity : TenantEntity<TReferenceNumber>
    {
        public QueryChildEntitiesOfTenantEntityQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.ReferenceNumber).NotEmpty();
            RuleFor(x => x.Selector).NotEmpty();
        }
    }

    public class QueryChildEntitiesOfTenantEntityQueryHandler<TEntity, TChildEntity, TReferenceNumber, TDto> : IRequestHandler<QueryChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>, DynamicQueryOutputDTO<TDto>>
    where TEntity : TenantEntity<TReferenceNumber>
    {
        private readonly ITenantEntityReadRepository<TEntity, TReferenceNumber> _tenantEntityReadRepository;

        public QueryChildEntitiesOfTenantEntityQueryHandler(ITenantEntityReadRepository<TEntity, TReferenceNumber> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<DynamicQueryOutputDTO<TDto>> Handle(QueryChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> query, CancellationToken cancellationToken)
        {
            IList<TDto> data = await this._tenantEntityReadRepository
                              .GetTenantEntityByReferenceNumber(query.TenantId, query.ReferenceNumber)
                              .SelectMany(query.Selector)
                              .ProjectToType<TDto>()
                              .ApplyFilters(query.QueryOptions)
                              .ToListAsync(cancellationToken);

            return new DynamicQueryOutputDTO<TDto>(query.QueryOptions, data);
        }
    }
}
