using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Interfaces;

public interface IChartOfAccountsService
{
    Task<PaginatedResultDto<ChartOfAccount>> GetPagedAsync(int page, int pageSize, string? find = null, bool? isPostable = null);
    Task<ChartOfAccount?> GetByCodeAsync(string code);
    Task CreateAsync(ChartOfAccount account);
    Task DeleteAsync(string code);
}
