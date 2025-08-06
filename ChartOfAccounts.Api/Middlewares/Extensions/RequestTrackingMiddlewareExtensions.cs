namespace ChartOfAccounts.Api.Middlewares.Extensions;

public static class RequestTrackingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTrackingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTrackingMiddleware>();
    }
}
