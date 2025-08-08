using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class ChildrenValidationHandler : BaseDeleteValidationHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public ChildrenValidationHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task ValidateAsync(string code)
    {
        if (await _repository.HasChildrenAsync(code))
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_HasChildren, code));

        if (NextHandler != null)
            await NextHandler.ValidateAsync(code);
    }
}