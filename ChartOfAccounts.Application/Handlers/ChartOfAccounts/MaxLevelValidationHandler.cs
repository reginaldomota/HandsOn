using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class MaxLevelValidationHandler : BaseCreateValidationHandler
{
    private const int MaxLevel = 256;

    public override async Task ValidateAsync(ChartOfAccount account)
    {
        if (account?.ParentCode?.Split(".").Count() >= MaxLevel)
            throw new BusinessRuleValidationException(
                string.Format(ErrorMessages.Error_ChartOfAccounts_Create_LimitReached, account.ParentCode));

        if (NextHandler != null)
            await NextHandler.ValidateAsync(account);
    }
}