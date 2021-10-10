﻿using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using OPLOGMicroservice.Infra.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetEntityById
{
    public class GetEntityByIdQuery<TEntity, TDto> : IRequest<TDto>
        where TEntity : Entity
        where TDto : class
    {
        public GetEntityByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetEntityByIdQueryValidator<TEntity, TDto> : AbstractValidator<GetEntityByIdQuery<TEntity, TDto>>
        where TEntity : Entity
        where TDto : class
    {
        public GetEntityByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public class GetEntityByIdQueryHandler<TEntity, TDto> : IRequestHandler<GetEntityByIdQuery<TEntity, TDto>, TDto>
      where TEntity : Entity
        where TDto : class
    {
        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

        public GetEntityByIdQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
        {
            _entityReadRepository = entityReadRepository;
        }

        public async Task<TDto> Handle(GetEntityByIdQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            TDto entity = await _entityReadRepository
                           .GetEntityById(query.Id)
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
