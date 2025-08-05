using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Models.Common;
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

    public async Task<PaginatedResult<ChartOfAccount>> GetPagedAsync(int page, int pageSize)
    {
        var (items, totalCount) = await _repository.GetPagedAsync(page, pageSize);

        return new PaginatedResult<ChartOfAccount>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public Task<ChartOfAccount?> GetByCodeAsync(string code) => _repository.GetByCodeAsync(code);

    public Task AddAsync(ChartOfAccount account) => _repository.AddAsync(account);

    public Task DeleteAsync(string code) => _repository.DeleteAsync(code);
}
