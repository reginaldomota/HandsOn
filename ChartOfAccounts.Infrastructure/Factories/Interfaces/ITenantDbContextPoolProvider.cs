using ChartOfAccounts.Infrastructure.Data;

namespace ChartOfAccounts.Infrastructure.Factories.Interfaces;

public interface ITenantDbContextPoolProvider
{
    AppDbContext GetDbContext(Guid tenantId);
}
