using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Interfaces;

public interface IChartOfAccountsService
{
    Task<PaginatedResultDto<ChartOfAccount>> GetPagedAsync(int page, int pageSize);
    Task<ChartOfAccount?> GetByCodeAsync(string code);
    Task AddAsync(ChartOfAccount account);
    Task DeleteAsync(string code);
}
