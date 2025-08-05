using ChartOfAccounts.CrossCutting.Context.Interfaces;

namespace ChartOfAccounts.CrossCutting.Context;

public class RequestContext : IRequestContext
{
    private static readonly AsyncLocal<string?> _tenantId = new();
    private static readonly AsyncLocal<string?> _requestIdentifier = new();

    public string? TenantId => _tenantId.Value;
    public string RequestIdentifier => _requestIdentifier.Value ?? Guid.NewGuid().ToString();

    public static void SetTenant(string tenantId) => _tenantId.Value = tenantId;
    public static void SetRequestIdentifier(string requestId) => _requestIdentifier.Value = requestId;
}
