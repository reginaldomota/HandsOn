using ChartOfAccounts.Application.DTOs.Common;
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
    private const int MaxLevel = 256;

    public ChartOfAccountsService(IChartOfAccountsRepository repository)
    {
        _repository = repository;
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
            if (account?.ParentCode?.Split(".").Count() >= MaxLevel)
                throw new BusinessRuleValidationException(
                    string.Format(ErrorMessages.Error_ChartOfAccounts_Create_LimitReached, account.ParentCode));

            string expectedParentCode = string.Join(".", account.Code.Split('.').SkipLast(1));
            if (account.ParentCode != expectedParentCode)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeMustStartWithParent, account.Code, account.ParentCode, expectedParentCode));

            ChartOfAccount? parent = await _repository.GetByCodeAsync(account.ParentCode!);

            if (parent?.IsPostable is null)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_InvalidParentCode, account.ParentCode));

            if (parent.IsPostable == true)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_ParentIsPostable, account.ParentCode));

            if (parent.Type != account.Type)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_InvalidParentType, account.ParentCode, parent.Type, account.Type));

            await _repository.CreateAsync(account);
        }
        catch (DataIntegrityViolationException ex)
        {
            ChartOfAccount? chekAccount = await _repository.GetByCodeAsync(account.Code);

            if (chekAccount != null &&
                chekAccount.IdempotencyKey == account.IdempotencyKey &&
                ContentHashGenerator.ComputeFor(account) == ContentHashGenerator.ComputeFor(chekAccount))
                return;

            if (chekAccount != null)
                throw new BusinessRuleValidationException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeAlreadyExists, account.Code), ex);

            chekAccount = await _repository.GetByIdempotencyKeyAsync(account.IdempotencyKey!.Value);

            if (chekAccount != null)
                throw new ErrorHttpRequestException(
                    string.Format(ErrorMessages.Error_ChartOfAccounts_IdempotencyConflict, account.IdempotencyKey), ex, (int)HttpStatusCode.Conflict, ErrorCode.Conflict);

            throw new ErrorHttpRequestException(ex.Message, ex, ex.StatusCode, ex.ErrorCode);
        }
    }

    public async Task DeleteAsync(string code)
    {
        if(await _repository.HasChildrenAsync(code))
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_HasChildren, code));

        await _repository.DeleteAsync(code);
    }
}
