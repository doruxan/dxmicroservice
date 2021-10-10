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
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.QueryTenantEntities
{
    public class QueryTenantEntitiesQuery<TEntity, TDto> : IRequest<DynamicQueryPagedDTO<TDto>>
        where TEntity : TenantEntity
    {
        public QueryTenantEntitiesQuery(Guid tenantId, DynamicQueryOptions queryOptions)
        {
            TenantId = tenantId;
            QueryOptions = queryOptions;
            new QueryTenantEntitiesQueryValidator<TEntity, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        public DynamicQueryOptions QueryOptions { get; set; }
    }

    public class QueryTenantEntitiesQueryValidator<TEntity, TDto> : AbstractValidator<QueryTenantEntitiesQuery<TEntity, TDto>>
         where TEntity : TenantEntity
    {
        public QueryTenantEntitiesQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }

    public class QueryTenantEntitiesQueryHandler<TEntity, TDto> : IRequestHandler<QueryTenantEntitiesQuery<TEntity, TDto>, DynamicQueryPagedDTO<TDto>>
    where TEntity : TenantEntity
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public QueryTenantEntitiesQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<DynamicQueryPagedDTO<TDto>> Handle(QueryTenantEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            List<TDto> data = await this._tenantEntityReadRepository
                                .GetAllByTenantId(query.TenantId)
                                .ProjectToType<TDto>()
                                .ApplyFilters(query.QueryOptions)
                                .ToListAsync(cancellationToken);
            return new DynamicQueryPagedDTO<TDto>(query.QueryOptions, data);
        }
    }
}
