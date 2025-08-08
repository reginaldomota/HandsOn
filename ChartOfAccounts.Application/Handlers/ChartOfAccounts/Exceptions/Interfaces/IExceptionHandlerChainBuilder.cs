using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions.Interfaces;

public interface IExceptionHandlerChainBuilder
{
    IExceptionHandler Build();
    Task HandleExceptionAsync(ChartOfAccount account, DataIntegrityViolationException exception);
}