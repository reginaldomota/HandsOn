using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Factories.Interfaces;

namespace ChartOfAccounts.Infrastructure.Factories;

public class TenantDbContextFactory : ITenantDbContextFactory
{
    private readonly IRequestContext _requestContext;
    private readonly ITenantDbContextPoolProvider _poolProvider;

    public TenantDbContextFactory(IRequestContext requestContext, ITenantDbContextPoolProvider poolProvider)
    {
        _requestContext = requestContext;
        _poolProvider = poolProvider;
    }

    public AppDbContext CreateDbContext()
    {
        Guid tenantId = _requestContext.TenantId!.Value;
        return _poolProvider.GetDbContext(tenantId);
    }
}
