using ChartOfAccounts.CrossCutting.Context.Interfaces;

namespace ChartOfAccounts.CrossCutting.Context;

public static class TenantContextHolder
{
    private static readonly AsyncLocal<Guid> _current = new();

    public static Guid TenantId
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}
