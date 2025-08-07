using ChartOfAccounts.CrossCutting.Context;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Infrastructure.Extensions;

public static class TenantQueryableExtensions
{
    public static IQueryable<T> ForCurrentTenant<T>(this IQueryable<T> queryable)
        where T : ITenantEntity
    {
        return queryable.Where(entity => entity.TenantId == TenantContextHolder.TenantId);
    }
}
