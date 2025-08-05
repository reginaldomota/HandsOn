namespace ChartOfAccounts.CrossCutting.Context;

public static class RequestContext
{
    private static readonly AsyncLocal<string?> _tenantId = new();
    private static readonly AsyncLocal<string?> _requestId = new();

    public static string? TenantId
    {
        get => _tenantId.Value;
        set => _tenantId.Value = value;
    }

    public static string? RequestId
    {
        get => _requestId.Value;
        set => _requestId.Value = value;
    }
}
