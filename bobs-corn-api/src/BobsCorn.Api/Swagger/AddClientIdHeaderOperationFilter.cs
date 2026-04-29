using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BobsCorn.Api.Swagger
{
    public class AddClientIdHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Client-Id",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Client identifier for rate limiting",
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
