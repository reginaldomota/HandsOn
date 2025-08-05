using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Domain.Interfaces;

public interface IChartOfAccountsRepository
{
    Task<(List<ChartOfAccount> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
    Task<ChartOfAccount?> GetByCodeAsync(string code);
    Task<bool?> IsPostableAsync(string code);
    Task AddAsync(ChartOfAccount account);
    Task DeleteAsync(string code);
    Task<List<string>> GetChildrenCodesAsync(string parentCode);
}
