using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class ParentCodeValidationHandler : BaseCreateValidationHandler
{
    public override async Task ValidateAsync(ChartOfAccount account)
    {
        string expectedParentCode = string.Join(".", account.Code.Split('.').SkipLast(1));
        if (account.ParentCode != expectedParentCode)
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeMustStartWithParent, 
                    account.Code, account.ParentCode, expectedParentCode));

        if (NextHandler != null)
            await NextHandler.ValidateAsync(account);
    }
}