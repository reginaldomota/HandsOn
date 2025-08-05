namespace ChartOfAccounts.Api.Middlewares.Extensions;

public static class TenantContextMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantContextMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TenantContextMiddleware>();
    }
}
