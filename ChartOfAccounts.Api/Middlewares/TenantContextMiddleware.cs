using ChartOfAccounts.CrossCutting.Context;
using System.Security.Claims;

namespace ChartOfAccounts.Api.Middlewares;

public class TenantContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantContextMiddleware> _logger;

    public TenantContextMiddleware(RequestDelegate next, ILogger<TenantContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        string? requestId = context.TraceIdentifier;
        RequestContext.RequestId = requestId;

        // TenantId extraído do JWT
        if (context.User.Identity?.IsAuthenticated == true)
        {
            Claim? tenantClaim = context.User.FindFirst("tenant");
            if (tenantClaim != null)
            {
                RequestContext.TenantId = tenantClaim.Value;
            }
        }

        await _next(context);
    }
}
