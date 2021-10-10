//using FluentValidation;
//using Mapster;
//using Microsoft.EntityFrameworkCore;
//using OPLOGMicroservice.Business.Core.Interfaces;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Threading.Tasks;

//namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllChildEntitiesOfTenantEntity
//{
//    public class GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> : IAsyncQuery<IEnumerable<TDto>>
//        where TEntity : TenantEntity<TReferenceNumber>
//    {
//        public GetAllChildEntitiesOfTenantEntityQuery(Guid tenantId, TReferenceNumber referenceNumber, Expression<Func<TEntity, IEnumerable<TChildEntity>>> selector)
//        {
//            TenantId = tenantId;
//            ReferenceNumber = referenceNumber;
//            Selector = selector;
//            new GetAllChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto>().ValidateAndThrow(this);
//        }

//        public Guid TenantId { get; set; }

//        public TReferenceNumber ReferenceNumber { get; set; }

//        public Expression<Func<TEntity, IEnumerable<TChildEntity>>> Selector { get; set; }
//    }

//    public class GetAllChildEntitiesOfTenantEntityQueryValidator<TEntity, TChildEntity, TReferenceNumber, TDto> : AbstractValidator<GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>>
//         where TEntity : TenantEntity<TReferenceNumber>
//    {
//        public GetAllChildEntitiesOfTenantEntityQueryValidator()
//        {
//            RuleFor(x => x.TenantId).NotEmpty();
//            RuleFor(x => x.ReferenceNumber).NotEmpty();
//            RuleFor(x => x.Selector).NotEmpty();
//        }
//    }

//    public class GetAllChildEntitiesOfTenantEntityQueryHandler<TEntity, TChildEntity, TReferenceNumber, TDto> : IAsyncQueryExecutor<GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto>, IEnumerable<TDto>>
//    where TEntity : TenantEntity<TReferenceNumber>
//    {
//        private readonly ITenantEntityReadRepository<TEntity, TReferenceNumber> _tenantEntityReadRepository;

//        public GetAllChildEntitiesOfTenantEntityQueryHandler(ITenantEntityReadRepository<TEntity, TReferenceNumber> tenantEntityReadRepository)
//        {
//            _tenantEntityReadRepository = tenantEntityReadRepository;
//        }
//        public IEnumerable<TDto> Execute(GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> query, CancellationToken cancellationToken)
//        {
//            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
//        }

//        public async Task<IEnumerable<TDto>> ExecuteAsync(GetAllChildEntitiesOfTenantEntityQuery<TEntity, TChildEntity, TReferenceNumber, TDto> query, CancellationToken cancellationToken)
//        {
//            return await this._tenantEntityReadRepository
//                            .GetTenantEntityByReferenceNumber(query.TenantId, query.ReferenceNumber)
//                            .SelectMany(query.Selector)
//                            .ProjectToType<TDto>()
//                            .ToListAsync(cancellationToken);
//        }
//    }
//}
