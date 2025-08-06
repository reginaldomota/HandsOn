using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.CrossCutting.Tenancy.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Factories.Interfaces;

namespace ChartOfAccounts.Infrastructure.Factories;

public class TenantDbContextFactory : ITenantDbContextFactory
{
    private readonly IRequestContext _requestContext;
    private readonly ITenantConnectionProvider _connectionProvider;

    public TenantDbContextFactory(IRequestContext requestContext, ITenantConnectionProvider connectionProvider)
    {
        _requestContext = requestContext;
        _connectionProvider = connectionProvider;
    }

    public AppDbContext CreateDbContext()
    {
        Guid tenantId = _requestContext.TenantId!.Value;
        string connectionString = _connectionProvider.GetConnectionString(tenantId);
        return new AppDbContext(connectionString);
    }
}
