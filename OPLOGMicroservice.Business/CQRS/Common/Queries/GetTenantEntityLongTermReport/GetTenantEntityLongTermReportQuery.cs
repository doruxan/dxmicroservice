using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace OPLOGMicroservice.Business.CQRS.Common.Queries.GetTenantEntityLongTermReport
{
    public class GetTenantEntityLongTermReportQuery<TEntity, TDto> : IRequest<IList<TDto>>
        where TEntity : TenantEntity
    {
        public GetTenantEntityLongTermReportQuery(Guid tenantId, DateTime startTime)
        {
            this.StartTime = startTime;
            this.TenantId = tenantId;

            new GetTenantEntityLongTermReportQueryValidator<TEntity, TDto>().ValidateAndThrow(this);
        }

        public Guid TenantId { get; set; }

        private DateTime _startTime { get; set; }

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value.ToUniversalTime();
            }
        }

        public DateTime EndTime
        {
            get
            {
                return _startTime.AddDays(90);
            }
        }
    }

    public class GetTenantEntityLongTermReportQueryValidator<TEntity, T> : AbstractValidator<GetTenantEntityLongTermReportQuery<TEntity, T>>
        where TEntity : TenantEntity
    {
        public GetTenantEntityLongTermReportQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.StartTime).NotEqual(default(DateTime));
        }
    }

    public class GetTenantEntityLongTermReportQueryHandler<TEntity, TDto> : IRequestHandler<GetTenantEntityLongTermReportQuery<TEntity, TDto>, IList<TDto>>
    where TEntity : TenantEntity
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public GetTenantEntityLongTermReportQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            this._tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<IList<TDto>> Handle(GetTenantEntityLongTermReportQuery<TEntity, TDto> query, CancellationToken cancellationToken)
        {
            List<TDto> report = await this._tenantEntityReadRepository
                                                   .GetAllByTenantId(query.TenantId)
                                                   .Where(x => x.CreatedAt >= query.StartTime && x.CreatedAt <= query.EndTime)
                                                   .ProjectToType<TDto>()
                                                   .ToListAsync(cancellationToken);

            return report;
        }
    }
}
