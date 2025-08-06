namespace ChartOfAccounts.CrossCutting.Tenancy.Interfaces;

public interface ITenantConnectionProvider
{
    string GetConnectionString(Guid tenantId);
}
