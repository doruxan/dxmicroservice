using System;

namespace OPLOGMicroservice.Business.CQRS.Common.DTOs
{
    public class TimeRangeInputDTO
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}