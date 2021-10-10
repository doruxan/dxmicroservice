using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllEntities
{
    public class GetAllEntitiesQuery<TEntity, TDto> : IRequest<IEnumerable<TDto>>
        where TEntity : Entity
    {
    }

    public class GetAllEntitiesQueryHandler<TEntity, TDto> : IRequestHandler<GetAllEntitiesQuery<TEntity, TDto>, IEnumerable<TDto>>
       where TEntity : Entity
    {
        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

        public GetAllEntitiesQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
        {
            _entityReadRepository = entityReadRepository;
        }

        public async Task<IEnumerable<TDto>> Handle(GetAllEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            return await _entityReadRepository
                                .GetAllEntities()
                                .ProjectToType<TDto>()
                                .ToListAsync(cancellationToken);
        }
    }
}
