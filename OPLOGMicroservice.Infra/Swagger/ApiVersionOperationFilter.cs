using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OPLOGMicroservice.Infra.Swagger
{
    [ExcludeFromCodeCoverage]
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation?.Parameters.FirstOrDefault(p => p.Name == "api-version");
            if (versionParameter != null)
            {
                operation.Parameters.Remove(versionParameter);
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                return;
            }

            OpenApiPaths paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }
        }
    }
}
