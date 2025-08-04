using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Domain.Interfaces;

public interface IChartOfAccountsRepository
{
    Task<IEnumerable<ChartOfAccount>> GetAllAsync();
    Task<ChartOfAccount?> GetByCodeAsync(string code);
    Task AddAsync(ChartOfAccount account);
    Task DeleteAsync(string code);
    Task<List<string>> GetChildrenCodesAsync(string parentCode);
}
