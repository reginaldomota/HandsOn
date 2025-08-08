using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public abstract class BaseExceptionHandler : IExceptionHandler
{
    protected IExceptionHandler? NextHandler;

    public IExceptionHandler? SetNext(IExceptionHandler handler)
    {
        NextHandler = handler;
        return handler;
    }

    public abstract Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception);
}