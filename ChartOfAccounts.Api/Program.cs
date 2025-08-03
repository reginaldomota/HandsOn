using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Lista de versões da API suportadas
var apiVersions = new[] { "v1" };

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configurado para múltiplas versões
builder.Services.AddSwaggerGen(options =>
{
    foreach (var version in apiVersions)
    {
        options.SwaggerDoc(version, new OpenApiInfo
        {
            Title = $"Api Plano de Contas {version.ToUpper()}",
            Version = version.Replace("v", "") + ".0.0",
            Description = $"Documentação da API versão {version.ToUpper()}"
        });
    }

    // Inclui somente endpoints que pertencem à versão correspondente
    options.DocInclusionPredicate((docName, apiDesc) =>
        apiDesc.GroupName == docName);
});

var app = builder.Build();

// Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var version in apiVersions)
    {
        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Api Plano de Contas {version.ToUpper()}");
    }

    options.RoutePrefix = "swagger";
});

// HTTP pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
