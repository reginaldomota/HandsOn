using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Factories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Net;

namespace ChartOfAccounts.Infrastructure.Tenancy;

public class TenantDbContextPoolProvider : ITenantDbContextPoolProvider
{
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<Guid, DbContextOptions<AppDbContext>> _optionsCache = new();

    public TenantDbContextPoolProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext GetDbContext(Guid tenantId)
    {
        DbContextOptions<AppDbContext> options = _optionsCache.GetOrAdd(tenantId, id =>
        {
            string? connectionString = _configuration.GetConnectionString($"Tenant_{tenantId}");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ErrorHttpRequestException($"Tenant '{tenantId}' não possui acesso aos recursos", (int)HttpStatusCode.Forbidden, Domain.Enums.ErrorCode.Forbidden );

            DbContextOptionsBuilder<AppDbContext> builder = new();
            builder.UseNpgsql(connectionString);

            return builder.Options;
        });

        return new AppDbContext(options); 
    }
}
