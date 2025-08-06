using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Helpers;
using ChartOfAccounts.Application.Interfaces;
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

    public ChartOfAccountsService(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResultDto<ChartOfAccount>> GetPagedAsync(int page, int pageSize)
    {
        var (items, totalCount) = await _repository.GetPagedAsync(page, pageSize);

        return new PaginatedResultDto<ChartOfAccount>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public Task<ChartOfAccount?> GetByCodeAsync(string code) => _repository.GetByCodeAsync(code);

    public async Task CreateAsync(ChartOfAccount account)
    {
        try
        {
            bool? isPostable = await _repository.IsPostableAsync(account.ParentCode!);

            if (isPostable is null)
                throw new BusinessRuleValidationException($"O código {account.ParentCode!} não existe ou não é um código pai válido.");

            if (isPostable == true)
                throw new BusinessRuleValidationException($"O código {account.ParentCode!} não aceita contas filhas pois ele permite lançamentos.");

            await _repository.CreateAsync(account);
        }
        catch(DataIntegrityViolationException ex)
        {
            ChartOfAccount? chekAccount = await _repository.GetByCodeAsync(account.Code);

            if (chekAccount != null && 
                chekAccount.IdempotencyKey == account.IdempotencyKey && 
                ContentHashGenerator.ComputeFor(account) == ContentHashGenerator.ComputeFor(chekAccount))
                return;

            if (chekAccount != null)
                throw new BusinessRuleValidationException($"O código {account.Code} já existe.", ex);

            chekAccount = await _repository.GetByIdempotencyKeyAsync(account.IdempotencyKey);

            if (chekAccount != null)
                throw new ErrorHttpRequestException($"houve um conflito entre a nova tentativa e o estado previamente registrado para o Idempotency-Key {account.IdempotencyKey}", ex, (int)HttpStatusCode.Conflict, ErrorCode.Conflict);

            throw new ErrorHttpRequestException(ex.Message, ex, ex.StatusCode, ex.ErrorCode);
        }
    }

    public Task DeleteAsync(string code) => _repository.DeleteAsync(code);
}
