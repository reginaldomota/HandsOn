using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public interface IExceptionHandler
{
    IExceptionHandler? SetNext(IExceptionHandler handler);
    Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception);
}