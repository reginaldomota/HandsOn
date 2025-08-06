using ChartOfAccounts.Infrastructure.Data;

namespace ChartOfAccounts.Infrastructure.Factories.Interfaces;

public interface ITenantDbContextFactory
{
    AppDbContext CreateDbContext();
}
