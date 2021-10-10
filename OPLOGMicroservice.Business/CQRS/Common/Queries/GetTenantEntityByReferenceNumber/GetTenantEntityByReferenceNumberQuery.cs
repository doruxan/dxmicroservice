﻿//using FluentValidation;
//using Mapster;
//using Microsoft.EntityFrameworkCore;
//using OPLOGMicroservice.Business.Core.Interfaces;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
//using OPLOGMicroservice.Infra.Exceptions;
//using System;
//using System.Threading;
//using System.Threading.Tasks;


//namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetTenantEntityByReferenceNumber
//{
//    public class GetTenantEntityByReferenceNumberQuery<TEntity, TReferenceNumber, TDto> : IAsyncQuery<TDto>
//        where TEntity : TenantEntity<TReferenceNumber>
//        where TDto : class
//    {
//        public GetTenantEntityByReferenceNumberQuery(Guid tenantId, TReferenceNumber referenceNumber)
//        {
//            TenantId = tenantId;
//            ReferenceNumber = referenceNumber;
//            new GetTenantEntityByReferenceNumberQueryValidator<TEntity, TReferenceNumber, TDto>().ValidateAndThrow(this);
//        }

//        public Guid TenantId { get; set; }

//        public TReferenceNumber ReferenceNumber { get; set; }
//    }

//    public class GetTenantEntityByReferenceNumberQueryValidator<TEntity, TReferenceNumber, TDto> : AbstractValidator<GetTenantEntityByReferenceNumberQuery<TEntity, TReferenceNumber, TDto>>
//        where TEntity : TenantEntity<TReferenceNumber>
//        where TDto : class
//    {
//        public GetTenantEntityByReferenceNumberQueryValidator()
//        {
//            RuleFor(x => x.TenantId).NotEmpty();
//            RuleFor(x => x.ReferenceNumber).NotEmpty();
//        }
//    }

//    public class GetTenantEntityByReferenceNumberQueryHandler<TEntity, TRerefenceNumber, TDto> : IAsyncQueryExecutor<GetTenantEntityByReferenceNumberQuery<TEntity, TRerefenceNumber, TDto>, TDto>
//    where TEntity : TenantEntity<TRerefenceNumber>
//        where TDto : class
//    {
//        private readonly ITenantEntityReadRepository<TEntity, TRerefenceNumber> _tenantEntityReadRepository;

//        public GetTenantEntityByReferenceNumberQueryHandler(ITenantEntityReadRepository<TEntity, TRerefenceNumber> tenantEntityReadRepository)
//        {
//            _tenantEntityReadRepository = tenantEntityReadRepository;
//        }

//        public TDto Execute(GetTenantEntityByReferenceNumberQuery<TEntity, TRerefenceNumber, TDto> query, CancellationToken cancellationToken)
//        {
//            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
//        }

//        public async Task<TDto> ExecuteAsync(GetTenantEntityByReferenceNumberQuery<TEntity, TRerefenceNumber, TDto> query, CancellationToken cancellationToken)
//        {
//            var entity = await _tenantEntityReadRepository
//                          .GetTenantEntityByReferenceNumber(query.TenantId, query.ReferenceNumber)
//                          .ProjectToType<TDto>()
//                          .FirstOrDefaultAsync(cancellationToken);

//            if (entity == null)
//            {
//                throw new ResourceNotFoundException(nameof(TEntity), nameof(query.ReferenceNumber), query.ReferenceNumber);
//            }

//            return entity;
//        }
//    }
//}
