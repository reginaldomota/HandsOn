using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Errors.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChartOfAccounts.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IErrorResponseFactory _errorResponseFactory;

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };


    public ErrorHandlerMiddleware(
        RequestDelegate next, 
        ILogger<ErrorHandlerMiddleware> logger,
        IErrorResponseFactory errorResponseFactory)
    {
        _next = next;
        _logger = logger;
        _errorResponseFactory = errorResponseFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            ErrorResponse response = _errorResponseFactory.Create(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            string json = JsonSerializer.Serialize(response, _jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
}
