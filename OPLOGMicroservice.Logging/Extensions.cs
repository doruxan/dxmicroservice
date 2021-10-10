using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace OPLOGMicroservice.Logging
{
    public static class Extensions
    {
        public static MvcOptions AddPerformanceTracking(this MvcOptions options)
        {
            if (options.Filters.Any(x => x.GetType() == typeof(PerormanceTrackingFilter)))
                throw new InvalidOperationException("PerormanceTrackingFilter is allredy added");

            options.Filters.Add(new PerormanceTrackingFilter("", ""));
            return options;
        }

        public static IApplicationBuilder UseOPLOGExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
