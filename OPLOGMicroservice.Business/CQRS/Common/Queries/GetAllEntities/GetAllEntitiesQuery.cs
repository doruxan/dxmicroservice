//using Mapster;
//using Microsoft.EntityFrameworkCore;
//using OPLOGMicroservice.Business.Core.Interfaces;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllEntities
//{
//    public class GetAllEntitiesQuery<TEntity, TDto> : IAsyncQuery<IEnumerable<TDto>>
//        where TEntity : Entity
//    {
//    }

//    public class GetAllEntitiesQueryHandler<TEntity, TDto> : IAsyncQueryExecutor<GetAllEntitiesQuery<TEntity, TDto>, IEnumerable<TDto>>
//       where TEntity : Entity
//    {
//        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

//        public GetAllEntitiesQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
//        {
//            _entityReadRepository = entityReadRepository;
//        }

//        public IEnumerable<TDto> Execute(GetAllEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
//        }

//        public async Task<IEnumerable<TDto>> ExecuteAsync(GetAllEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return await _entityReadRepository
//                                .GetAllEntities()
//                                .ProjectToType<TDto>()
//                                .ToListAsync(cancellationToken);
//        }
//    }
//}
