using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace OPLOGMicroservice.Infra.Swagger
{
    public class TenantIdOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Tenant-Id",
                Description = "If you want to emulate, please enter TenantId.",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}