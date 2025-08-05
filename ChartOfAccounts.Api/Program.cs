using ChartOfAccounts.Api.Config.DependencyInjection;
using ChartOfAccounts.Api.Middlewares.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

    OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Informe o token JWT no formato: Bearer {seu_token}",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme,
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ChartOfAccounts.Auth",
            ValidAudience = "ChartOfAccounts.ApiClients",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("d8afacb3-bf57-45ae-a6c3-174e8422ee78"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.RegisterAllDependencies(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

app.UseErrorHandlerMiddleware();
app.UseTenantContextMiddleware();

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
