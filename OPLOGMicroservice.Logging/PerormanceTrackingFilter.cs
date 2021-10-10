using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace OPLOGMicroservice.Logging
{
    public class PerormanceTrackingFilter : IActionFilter
    {
        private PerformanceTracker _tracker;
        private string _product, _layer;
        public PerormanceTrackingFilter(string product, string layer)
        {
            _product = product;
            _layer = layer;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var activity = $"{request.Path}-{request.Method}";

            var dict = new Dictionary<string, object>();
            foreach (var key in context.RouteData.Values?.Keys)
                dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);

            var details = LogHelper.GetWebLogDetail(_product, _layer, activity,
                context.HttpContext, dict);

            _tracker = new PerformanceTracker(details);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_tracker != null)
                _tracker.Stop();

        }
    }
}
