// <copyright file="WeeklyReportOutputDTO.cs" company="Oplog">
// Copyright (c) Oplog. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OPLOGMicroservice.Business.CQRS.Common.Queries
{
    public class WeeklyReportOutputDTO
    {
        public WeeklyReportOutputDTO(List<DailyReportOutputDTO> entities, DateTime startTime)
        {
            for (int i = 1; i <= 7; i++)
            {
                DateTime day = startTime.Add(TimeSpan.FromDays(i)).Date;
                if (entities.FirstOrDefault(x => x.Date == day) == null)
                {
                    entities.Add(new DailyReportOutputDTO(day, 0));
                }
            }

            Report = entities.OrderBy(e => e.Date).ToList();
        }

        public IList<DailyReportOutputDTO> Report { get; set; }
    }

    public class DailyReportOutputDTO
    {
        public DailyReportOutputDTO()
        {
        }

        public DailyReportOutputDTO(DateTime date, int count)
        {
            Date = date;
            Count = count;
        }

        public static Expression<Func<IGrouping<DateTime, Entity>, DailyReportOutputDTO>> Projection
        {
            get
            {
                return grouping => new DailyReportOutputDTO
                {
                    Date = grouping.Key,
                    Count = grouping.Count()
                };
            }
        }

        [JsonIgnore]
        public DateTime Date { get; set; }

        public int Count { get; set; }

        public DayOfWeek DayOfWeek { get { return Date.DayOfWeek; } }
    }
}
