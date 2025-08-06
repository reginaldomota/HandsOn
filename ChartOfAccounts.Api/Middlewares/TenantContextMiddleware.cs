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
        if (context.User.Identity?.IsAuthenticated == true)
        {
            Claim? tenantClaim = context.User.FindFirst("tenant");
            if (tenantClaim != null)
            {
                RequestContext.SetTenant(Guid.Parse(tenantClaim.Value));
            }
        }

        await _next(context);
    }
}
