//using FluentValidation;
//using Mapster;
//using Microsoft.EntityFrameworkCore;
//using OPLOGMicroservice.Business.Core.Interfaces;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
//using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
//using System;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Threading.Tasks;

//namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetChildEntityOfEntity
//{
//    public class GetChildEntityOfEntityQuery<TEntity, TChildEntity, TDto> : IAsyncQuery<TDto>
//        where TEntity : Entity
//        where TDto : class
//    {
//        public GetChildEntityOfEntityQuery(Guid id, Expression<Func<TEntity, TChildEntity>> selector)
//        {
//            Id = id;
//            Selector = selector;
//            new GetChildEntityOfEntityQueryValidator<TEntity, TChildEntity, TDto>().ValidateAndThrow(this);
//        }

//        public Guid Id { get; set; }

//        public Expression<Func<TEntity, TChildEntity>> Selector { get; set; }
//    }

//    public class GetChildEntityOfEntityQueryValidator<TEntity, TChildEntity, TDto> : AbstractValidator<GetChildEntityOfEntityQuery<TEntity, TChildEntity, TDto>>
//         where TEntity : Entity
//         where TDto : class
//    {
//        public GetChildEntityOfEntityQueryValidator()
//        {
//            RuleFor(x => x.Id).NotEmpty();
//            RuleFor(x => x.Selector).NotEmpty();
//        }
//    }

//    public class GetChildEntityOfEntityQueryHandler<TEntity, TChildEntity, TDto> : IAsyncQueryExecutor<GetChildEntityOfEntityQuery<TEntity, TChildEntity, TDto>, TDto>
//       where TEntity : Entity
//       where TDto : class
//    {
//        private readonly IEntityReadRepository<TEntity> _entityReadRepository;

//        public GetChildEntityOfEntityQueryHandler(IEntityReadRepository<TEntity> entityReadRepository)
//        {
//            _entityReadRepository = entityReadRepository;
//        }

//        public TDto Execute(GetChildEntityOfEntityQuery<TEntity, TChildEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return Task.Run(async () => { return await ExecuteAsync(query, cancellationToken).ConfigureAwait(false); }).Result;
//        }

//        public async Task<TDto> ExecuteAsync(GetChildEntityOfEntityQuery<TEntity, TChildEntity, TDto> query, CancellationToken cancellationToken)
//        {
//            return await this._entityReadRepository
//                                .GetEntityById(query.Id)
//                                .Select(query.Selector)
//                                .ProjectToType<TDto>()
//                                .FirstOrDefaultAsync(cancellationToken);
//        }
//    }
//}
