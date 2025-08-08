using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class ParentTypeValidationHandler : BaseCreateValidationHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public ParentTypeValidationHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task ValidateAsync(ChartOfAccount account)
    {
        ChartOfAccount? parent = await _repository.GetByCodeAsync(account.ParentCode!);

        if (parent.Type != account.Type)
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_InvalidParentType, 
                    account.ParentCode, parent.Type, account.Type));

        if (NextHandler != null)
            await NextHandler.ValidateAsync(account);
    }
}