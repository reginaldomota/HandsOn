using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class ParentExistenceValidationHandler : BaseCreateValidationHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public ParentExistenceValidationHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task ValidateAsync(ChartOfAccount account)
    {
        ChartOfAccount? parent = await _repository.GetByCodeAsync(account.ParentCode!);

        if (parent?.IsPostable is null)
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_InvalidParentCode, account.ParentCode));

        if (NextHandler != null)
            await NextHandler.ValidateAsync(account);
    }
}