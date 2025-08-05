namespace ChartOfAccounts.CrossCutting.Context.Interfaces;

public interface IRequestContext
{
    string? TenantId { get; }
    string RequestId { get; }
}
