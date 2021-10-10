using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllTenantEntities
{
    public class GetAllTenantEntitiesQuery<TEntity, TDto> : IRequest<IEnumerable<TDto>>
        where TEntity : TenantEntity
    {
        public GetAllTenantEntitiesQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }

        public Guid TenantId { get; set; }
    }

    public class GetAllTenantEntitiesQueryValidator<TEntity, TDto> : AbstractValidator<GetAllTenantEntitiesQuery<TEntity, TDto>>
        where TEntity : TenantEntity
    {
        public GetAllTenantEntitiesQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }

    public class GetAllTenantEntitiesQueryHandler<TEntity, TDto> : IRequestHandler<GetAllTenantEntitiesQuery<TEntity, TDto>, IEnumerable<TDto>>
     where TEntity : TenantEntity
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public GetAllTenantEntitiesQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<IEnumerable<TDto>> Handle(GetAllTenantEntitiesQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            return await _tenantEntityReadRepository
                                .GetAllByTenantId(query.TenantId)
                                .ProjectToType<TDto>()
                                .ToListAsync(cancellationToken);
        }
    }
}
