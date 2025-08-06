using ChartOfAccounts.CrossCutting.Tenancy.Interfaces;
using ChartOfAccounts.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net;

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
               ?? throw new ErrorHttpRequestException($"Tenant '{tenantId}' não possui acesso ou não está configurado.", (int)HttpStatusCode.Forbidden, Domain.Enums.ErrorCode.Forbidden);
    }
}
