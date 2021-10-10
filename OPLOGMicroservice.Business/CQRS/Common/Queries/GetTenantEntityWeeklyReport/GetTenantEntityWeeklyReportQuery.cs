using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OPLOGMicroservice.Business.CQRS.Common.Queries;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.CQRS.Comon.Queries.GetTenantEntityWeeklyReport
{
    public class GetTenantEntityWeeklyReportQuery<TEntity> : IRequest<WeeklyReportOutputDTO>
        where TEntity : TenantEntity
    {
        public GetTenantEntityWeeklyReportQuery(Guid tenantId, DateTime startTime, TimeZoneInfo timeZoneInfo)
        {
            this.StartTime = startTime;
            this.TenantId = tenantId;
            this.TimeZoneInfo = timeZoneInfo;

            new GetTenantEntityWeeklyReportQueryValidator<TEntity>().ValidateAndThrow(this);
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
                return _startTime.AddDays(7);
            }
        }

        public TimeZoneInfo TimeZoneInfo { get; set; }
    }

    public class GetTenantEntityWeeklyReportQueryValidator<TEntity> : AbstractValidator<GetTenantEntityWeeklyReportQuery<TEntity>>
        where TEntity : TenantEntity
    {
        public GetTenantEntityWeeklyReportQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.StartTime).NotEqual(default(DateTime));
            RuleFor(x => x.TimeZoneInfo).NotEqual(default(TimeZoneInfo));
        }
    }

    public class GetTenantEntityWeeklyReportQueryHandler<TEntity> : IRequestHandler<GetTenantEntityWeeklyReportQuery<TEntity>, WeeklyReportOutputDTO>
    where TEntity : TenantEntity
    {
        private readonly ITenantEntityReadRepository<TEntity> _tenantEntityReadRepository;

        public GetTenantEntityWeeklyReportQueryHandler(ITenantEntityReadRepository<TEntity> tenantEntityReadRepository)
        {
            this._tenantEntityReadRepository = tenantEntityReadRepository;
        }

        public async Task<WeeklyReportOutputDTO> Handle(GetTenantEntityWeeklyReportQuery<TEntity> query, CancellationToken cancellationToken)
        {
            List<DailyReportOutputDTO> report = await this._tenantEntityReadRepository
                                                 .GetAllByTenantId(query.TenantId)
                                                 .Where(x => x.CreatedAt >= query.StartTime && x.CreatedAt <= query.EndTime)
                                                 .GroupBy(x => x.CreatedAt.AddHours(query.TimeZoneInfo.BaseUtcOffset.Hours).Date)
                                                 .Select(DailyReportOutputDTO.Projection)
                                                 .ToListAsync(cancellationToken);

            return new WeeklyReportOutputDTO(report, query.StartTime);
        }
    }
}
