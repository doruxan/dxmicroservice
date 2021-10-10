//using FluentValidation;
//using Mapster;
//using Microsoft.EntityFrameworkCore;
//using OPLOGMicroservice.Business.Core.Interfaces;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllTenantEntities
//{
//    public class GetAllTenantEntitiesQuery<TEntity, TDto> : IAsyncQuery<IEnumerable<TDto>>
//        where TEntity : TenantEntity
//    {
//        public GetAllTenantEntitiesQuery(Guid tenantId)
//        {
//            TenantId = tenantId;
//        }

//        public Guid TenantId { get; set; }
//    }

//    public class GetAllTenantEntitiesQueryValidator<TEntity, TDto> : AbstractValidator<GetAllTenantEntitiesQuery<TEntity, TDto>>
//        where TEntity : TenantEntity
//    {
//        public GetAllTenantEntitiesQueryValidator()
//        {
//            RuleFor(x => x.TenantId).NotEmpty();
//        }
//    }

//    public class GetAllTenantEntitiesQueryHandler<TEntity, TDto> : IAsyncQueryExecutor<GetAllTenantEntitiesQuery<TEntity, TDto>, IEnumerable<TDto>>
//     where TEntity : TenantEntity
//    {
//        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

//        public GetAllTenantEntitiesQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
//        {
//            _tenantEntityReadRepository = tenantEntityReadRepository;
//        }

//        public IEnumerable<TDto> Execute(GetAllTenantEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
//        }

//        public async Task<IEnumerable<TDto>> ExecuteAsync(GetAllTenantEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return await _tenantEntityReadRepository
//                                .GetAllByTenantId(query.TenantId)
//                                .ProjectToType<TDto>()
//                                .ToListAsync(cancellationToken);
//        }
//    }
//}
