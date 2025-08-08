using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions.Interfaces;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Domain.Services;
using System.Net;

namespace ChartOfAccounts.Application.Services;

public class ChartOfAccountsService : IChartOfAccountsService
{
    private readonly IChartOfAccountsRepository _repository;
    private readonly IDeleteValidationChainBuilder _deleteValidationChainBuilder;
    private readonly ICreateValidationChainBuilder _createValidationChainBuilder;
    private readonly IExceptionHandlerChainBuilder _exceptionHandlerChainBuilder;
    private const int MaxLevel = 256;

    public ChartOfAccountsService(
        IChartOfAccountsRepository repository,
        IDeleteValidationChainBuilder deleteValidationChainBuilder,
        ICreateValidationChainBuilder createValidationChainBuilder,
        IExceptionHandlerChainBuilder exceptionHandlerChainBuilder)
    {
        _repository = repository;
        _deleteValidationChainBuilder = deleteValidationChainBuilder;
        _createValidationChainBuilder = createValidationChainBuilder;
        _exceptionHandlerChainBuilder = exceptionHandlerChainBuilder;
    }

    public async Task<PaginatedResultDto<ChartOfAccount>> GetPagedAsync(int page, int pageSize, string? find = null, bool? isPostable = null)
    {
        (List<ChartOfAccount> items, int totalCount) = await _repository.GetPagedAsync(page, pageSize, find, isPostable);

        return new PaginatedResultDto<ChartOfAccount>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items
        };
    }

    public Task<ChartOfAccount?> GetByCodeAsync(string code) => _repository.GetByCodeAsync(code);

    public async Task CreateAsync(ChartOfAccount account)
    {
        try
        {
            await _createValidationChainBuilder.ValidateAsync(account);
            await _repository.CreateAsync(account);
        }
        catch (DataIntegrityViolationException ex)
        {
            await _exceptionHandlerChainBuilder.HandleExceptionAsync(account, ex);
        }
    }

    public async Task DeleteAsync(string code)
    {
        await _deleteValidationChainBuilder.ValidateAsync(code);
        await _repository.DeleteAsync(code);
    }
}
