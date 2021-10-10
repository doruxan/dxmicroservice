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


namespace OPLOGMicroservice.Business.CQRS.Common.Queries.QueryChildEntitiesOfEntity
{
    public class QueryChildEntitiesOfEntityQuery<TEntity, TChildEntity, TDto> : IRequest<DynamicQueryOutputDTO<TDto>>
        where TEntity : Entity
    {
        public QueryChildEntitiesOfEntityQuery(DynamicQueryOptions queryOptions, Expression<Func<TEntity, IEnumerable<TChildEntity>>> selector)
        {
            QueryOptions = queryOptions;
            Selector = selector;
            new QueryChildEntitiesOfEntityQueryValidator<TEntity, TChildEntity, TDto>().ValidateAndThrow(this);
        }

        public DynamicQueryOptions QueryOptions { get; set; }

        public Expression<Func<TEntity, IEnumerable<TChildEntity>>> Selector { get; set; }
    }

    public class QueryChildEntitiesOfEntityQueryValidator<TEntity, TChildEntity, TDto> : AbstractValidator<QueryChildEntitiesOfEntityQuery<TEntity, TChildEntity, TDto>>
         where TEntity : Entity
    {
        public QueryChildEntitiesOfEntityQueryValidator()
        {
            RuleFor(x => x.Selector).NotEmpty();
        }
    }

    public class QueryChildEntitiesEntityQueryHandler<TEntity, TChildEntity, TDto> : IRequestHandler<QueryChildEntitiesOfEntityQuery<TEntity, TChildEntity, TDto>, DynamicQueryOutputDTO<TDto>>
    where TEntity : Entity
    {
        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

        public QueryChildEntitiesEntityQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
        {
            _entityReadRepository = entityReadRepository;
        }

        public async Task<DynamicQueryOutputDTO<TDto>> Handle(QueryChildEntitiesOfEntityQuery<TEntity, TChildEntity, TDto> query, CancellationToken cancellationToken)
        {
            IList<TDto> data = await this._entityReadRepository
                               .GetAllEntities()
                               .SelectMany(query.Selector)
                               .ProjectToType<TDto>()
                               .ApplyFilters(query.QueryOptions)
                               .ToListAsync(cancellationToken);

            return new DynamicQueryOutputDTO<TDto>(query.QueryOptions, data);
        }
    }
}
