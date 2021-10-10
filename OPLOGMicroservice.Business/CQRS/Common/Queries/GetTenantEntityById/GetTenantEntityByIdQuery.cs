using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using OPLOGMicroservice.Infra.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetTenantEntityById
{
    public class GetTenantEntityByIdQuery<TEntity, TDto> : IRequest<TDto>
        where TEntity : TenantEntity
        where TDto : class
    {
        public GetTenantEntityByIdQuery(Guid tenantId, Guid id)
        {
            TenantId = tenantId;
            Id = id;

            new GetTenantEntityByIdQueryValidator<TEntity, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        public Guid Id { get; set; }
    }

    public class GetTenantEntityByIdQueryValidator<TEntity, TDto> : AbstractValidator<GetTenantEntityByIdQuery<TEntity, TDto>>
        where TEntity : TenantEntity
        where TDto : class
    {
        public GetTenantEntityByIdQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class GetTenantEntityByIdQueryHandler<TEntity, TDto> : IRequestHandler<GetTenantEntityByIdQuery<TEntity, TDto>, TDto>
      where TEntity : TenantEntity
        where TDto : class
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public GetTenantEntityByIdQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            _tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<TDto> Handle(GetTenantEntityByIdQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            TDto entity = await _tenantEntityReadRepository
                           .GetTenantEntityById(query.TenantId, query.Id)
                           .ProjectToType<TDto>()
                           .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new ResourceNotFoundException(nameof(TEntity), nameof(query.Id), query.Id);
            }

            return entity;
        }
    }
}