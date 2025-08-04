using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Application.Services;

public class ChartOfAccountsService : IChartOfAccountsService
{
    private readonly IChartOfAccountsRepository _repository;

    public ChartOfAccountsService(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<ChartOfAccount>> GetAllAsync() => _repository.GetAllAsync();

    public Task<ChartOfAccount?> GetByCodeAsync(string code) => _repository.GetByCodeAsync(code);

    public Task AddAsync(ChartOfAccount account) => _repository.AddAsync(account);

    public Task DeleteAsync(string code) => _repository.DeleteAsync(code);
}
