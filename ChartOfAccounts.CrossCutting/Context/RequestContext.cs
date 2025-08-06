using ChartOfAccounts.CrossCutting.Context.Interfaces;

namespace ChartOfAccounts.CrossCutting.Context;

public class RequestContext : IRequestContext
{
    private static readonly AsyncLocal<Guid?> _tenantId = new();
    private static readonly AsyncLocal<string?> _requestIdentifier = new();
    private static readonly AsyncLocal<Guid?> _idempotencyKey = new();

    public Guid? TenantId => _tenantId.Value;
    public string RequestId => _requestIdentifier.Value ?? RequestIdentifierGenerator.Generate();
    public Guid? IdempotencyKey => _idempotencyKey.Value;

    public static void SetTenant(Guid tenantId) => _tenantId.Value = tenantId;
    public static void SetRequestIdentifier(string requestId) => _requestIdentifier.Value = requestId;
    public static void SetIdempotencyKey(Guid idempotencyKey) => _idempotencyKey.Value = idempotencyKey;
}
