using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public abstract class BaseDeleteValidationHandler : IDeleteValidationHandler
{
    protected IDeleteValidationHandler? NextHandler;

    public IDeleteValidationHandler? SetNext(IDeleteValidationHandler handler)
    {
        NextHandler = handler;
        return handler;
    }

    public abstract Task ValidateAsync(string code);
}