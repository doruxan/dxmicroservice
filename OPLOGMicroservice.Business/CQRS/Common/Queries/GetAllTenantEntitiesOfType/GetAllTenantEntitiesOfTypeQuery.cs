using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetAllTenantEntitiesOfType
{
    public class GetAllTenantEntitiesOfTypeQuery<TEntity, TType, TDto> : IRequest<IEnumerable<TDto>>
        where TEntity : TenantEntity
    {
        public GetAllTenantEntitiesOfTypeQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }

        public Guid TenantId { get; set; }
    }

    public class GetAllTenantEntitiesOfTypeQueryValidator<TEntity, TType, TDto> : AbstractValidator<GetAllTenantEntitiesOfTypeQuery<TEntity, TType, TDto>>
        where TEntity : TenantEntity
    {
        public GetAllTenantEntitiesOfTypeQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }

    public class GetAllTenantEntitiesOfTypeQueryHandler<TEntity, TType, TDto> : IRequestHandler<GetAllTenantEntitiesOfTypeQuery<TEntity, TType, TDto>, IEnumerable<TDto>>
     where TEntity : TenantEntity
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public GetAllTenantEntitiesOfTypeQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<IEnumerable<TDto>> Handle(GetAllTenantEntitiesOfTypeQuery<TEntity, TType, TDto> query, CancellationToken cancellationToken)
        {
            return await _tenantEntityReadRepository
                                .GetAllByTenantId(query.TenantId)
                                .OfType<TType>()
                                .ProjectToType<TDto>()
                                .ToListAsync(cancellationToken);
        }
    }
}