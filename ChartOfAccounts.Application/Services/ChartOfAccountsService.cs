using ChartOfAccounts.Application.Helpers;
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

    public async Task AddAsync(ChartOfAccount account)
    {
        bool? isPostable = await _repository.IsPostableAsync(account.ParentCode!);

        if (isPostable is null)
            throw new InvalidOperationException($"O código {account.ParentCode!} não existe ou não é um código pai válido."); //todo criar excessao customizada e consumir no middleware. criar resource

        if (isPostable == true)
            throw new InvalidOperationException($"O código {account.ParentCode!} não aceita contas filhas pois ele permite lançamentos."); //todo criar excessao customizada e consumir no middleware

        await _repository.AddAsync(account);
    }

    public Task DeleteAsync(string code) => _repository.DeleteAsync(code);
}
