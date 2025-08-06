using ChartOfAccounts.CrossCutting.Tenancy.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ChartOfAccounts.Infrastructure.Tenancy;

public class TenantConnectionProvider : ITenantConnectionProvider
{
    private readonly IConfiguration _configuration;

    public TenantConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(Guid tenantId)
    {
        return _configuration.GetConnectionString($"Tenant_{tenantId}")
               ?? throw new InvalidOperationException($"Tenant '{tenantId}' não configurado.");
    }
}
