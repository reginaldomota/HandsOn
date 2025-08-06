namespace ChartOfAccounts.CrossCutting.Context.Interfaces;

public interface IRequestContext
{
    Guid? TenantId { get; }
    string RequestId { get; }
    Guid? IdempotencyKey { get; }
}
