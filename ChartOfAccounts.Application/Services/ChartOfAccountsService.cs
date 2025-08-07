using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Domain.Services;
using System.Net;
using ChartOfAccounts.CrossCutting.Resources;

namespace ChartOfAccounts.Application.Services;

public class ChartOfAccountsService : IChartOfAccountsService
{
    private readonly IChartOfAccountsRepository _repository;
    private const int MaxLevel = 5;

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
            if(account?.ParentCode?.Split(".").Count() >= MaxLevel)
                throw new BusinessRuleValidationException(
                    string.Format(ErrorMessages.Error_ChartOfAccounts_Create_LimitReached, account.ParentCode));

            bool? isPostable = await _repository.IsPostableAsync(account.ParentCode!);

            if (isPostable is null)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_InvalidParentCode, account.ParentCode));

            if (isPostable == true)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_ParentIsPostable, account.ParentCode));

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
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeAlreadyExists, account.Code), ex);

            chekAccount = await _repository.GetByIdempotencyKeyAsync(account.IdempotencyKey);

            if (chekAccount != null)
                throw new ErrorHttpRequestException(
                    string.Format(ErrorMessages.Error_ChartOfAccounts_IdempotencyConflict, account.IdempotencyKey), ex, (int)HttpStatusCode.Conflict, ErrorCode.Conflict);

            throw new ErrorHttpRequestException(ex.Message, ex, ex.StatusCode, ex.ErrorCode);
        }
    }

    public Task DeleteAsync(string code) => _repository.DeleteAsync(code);
}
