using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public class DefaultExceptionHandler : BaseExceptionHandler
{
    public override async Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception)
    {
        throw new ErrorHttpRequestException(exception.Message, exception, exception.StatusCode, exception.ErrorCode);
    }
}