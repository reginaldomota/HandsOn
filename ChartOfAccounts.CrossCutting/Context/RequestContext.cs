using ChartOfAccounts.CrossCutting.Context.Interfaces;

namespace ChartOfAccounts.CrossCutting.Context;

public class RequestContext : IRequestContext
{
    private static readonly AsyncLocal<string?> _tenantId = new();
    private static readonly AsyncLocal<string?> _requestIdentifier = new();
    private static readonly AsyncLocal<string?> _idempotencyKey = new();

    public string? TenantId => _tenantId.Value;
    public string RequestId => _requestIdentifier.Value ?? RequestIdentifierGenerator.Generate();
    public string? IdempotencyKey => _idempotencyKey.Value;

    public static void SetTenant(string tenantId) => _tenantId.Value = tenantId;
    public static void SetRequestIdentifier(string requestId) => _requestIdentifier.Value = requestId;
    public static void SetIdempotencyKey(string idempotencyKey) => _idempotencyKey.Value = idempotencyKey;
}
