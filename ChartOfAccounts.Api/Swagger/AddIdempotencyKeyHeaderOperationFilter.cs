using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChartOfAccounts.Api.Swagger;

public class AddIdempotencyKeyHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod?.ToUpper() != "POST")
            return;

        // Exclui o endpoint de autenticação
        var relativePath = context.ApiDescription.RelativePath?.ToLowerInvariant();
        if (relativePath == "api/v1/auth/token")
            return;

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Idempotency-Key",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string"
            },
            Description = "Chave idempotente para evitar múltiplas criações do mesmo recurso"
        });
    }
}
