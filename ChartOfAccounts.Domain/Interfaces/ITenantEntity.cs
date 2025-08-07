namespace ChartOfAccounts.Domain.Interfaces;

public interface ITenantEntity
{
    Guid? TenantId { get; set; }
}
