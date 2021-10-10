using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Logging
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            LogHelper.LogWebError(ex, httpContext);
            var errorId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            return httpContext.Response.WriteAsync(new ErrorResultModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
                ErrorId = errorId,
            }.ToString());
        }
    }
}