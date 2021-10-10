using DynamicQueryBuilder;
using DynamicQueryBuilder.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Business.CQRS.Common.DTOs;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.QueryEntities
{
    public class QueryEntitiesQuery<TEntity, TDto> : IAsyncQuery<DynamicQueryOutputDTO<TDto>>
        where TEntity : Entity
    {
        public QueryEntitiesQuery(DynamicQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
        }

        public DynamicQueryOptions QueryOptions { get; set; }
    }

    public class QueryEntitiesQueryHandler<TEntity, TDto> : IAsyncQueryExecutor<QueryEntitiesQuery<TEntity, TDto>, DynamicQueryOutputDTO<TDto>>
     where TEntity : Entity
    {
        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

        public QueryEntitiesQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
        {
            _entityReadRepository = entityReadRepository;
        }

        public DynamicQueryOutputDTO<TDto> Execute(QueryEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
        }

        public async Task<DynamicQueryOutputDTO<TDto>> ExecuteAsync(QueryEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            List<TDto> data = await this._entityReadRepository
                                .GetAllEntities()
                                .ProjectToType<TDto>()
                                .ApplyFilters(query.QueryOptions)
                                .ToListAsync(cancellationToken);

            return new DynamicQueryOutputDTO<TDto>(query.QueryOptions, data);
        }
    }
}
