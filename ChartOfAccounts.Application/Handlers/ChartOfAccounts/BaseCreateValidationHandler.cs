using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public abstract class BaseCreateValidationHandler : ICreateValidationHandler
{
    protected ICreateValidationHandler? NextHandler;

    public ICreateValidationHandler? SetNext(ICreateValidationHandler handler)
    {
        NextHandler = handler;
        return handler;
    }

    public abstract Task ValidateAsync(ChartOfAccount account);
}