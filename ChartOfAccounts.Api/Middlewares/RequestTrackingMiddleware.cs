using ChartOfAccounts.CrossCutting.Context;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using System.Diagnostics;

namespace ChartOfAccounts.Api.Middlewares;

public class RequestTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        string? requestId = RequestIdentifierGenerator.Generate();
        RequestContext.SetRequestIdentifier(requestId);

        if (context.Request.Headers.TryGetValue("Idempotency-Key", out var idempotencyKeyHeader))
        {
            if (!Guid.TryParse(idempotencyKeyHeader, out Guid idempotencyKeyGuid))
                throw new ErrorHttpRequestException("Cabeçalho Header Idempotency-Key inválido. Deve ser um GUID válido no formato UUIDv4", StatusCodes.Status400BadRequest, ErrorCode.ValidationError);

            RequestContext.SetIdempotencyKey(idempotencyKeyGuid);
        }

        string? idempotencyKey = idempotencyKeyHeader;

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            context.Response.Headers["x-Idempotency-Key"] = idempotencyKey;
            context.Response.Headers["x-Request-Identifier"] = requestId;
            context.Response.Headers["x-response-time"] = $"{stopwatch.ElapsedMilliseconds}ms";

            return Task.CompletedTask;
        });

        await _next(context);
    }
}
