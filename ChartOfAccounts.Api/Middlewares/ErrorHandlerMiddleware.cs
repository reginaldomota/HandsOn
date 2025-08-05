using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChartOfAccounts.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };


    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ServiceUnavailableException ex)
        {
            _logger.LogWarning(ex, "Service unavailable: {Message}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            ErrorResponse response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                ErrorCode = ex.ErrorCode.ToString(),
                RequestIdentifier = ex.RequestIdentifier
            };
            string json = JsonSerializer.Serialize(response, _jsonOptions);
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorResponse response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                ErrorCode = ErrorCode.InternalServerError.ToString(),
                Message = "Ocorreu um erro inesperado.",
#if DEBUG
                Details = ex.ToString()
#endif
            };

            string json = JsonSerializer.Serialize(response, _jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
}
