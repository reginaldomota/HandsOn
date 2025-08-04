using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Interfaces;

public interface IChartOfAccountsService
{
    Task<IEnumerable<ChartOfAccount>> GetAllAsync();
    Task<ChartOfAccount?> GetByCodeAsync(string code);
    Task AddAsync(ChartOfAccount account);
    Task DeleteAsync(string code);
}
