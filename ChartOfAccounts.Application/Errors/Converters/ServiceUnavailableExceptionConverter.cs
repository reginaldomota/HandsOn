using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Domain.Exceptions;

namespace ChartOfAccounts.Application.Errors.Converters;

public class ServiceUnavailableExceptionConverter : IErrorResponseConverter
{
    public bool CanHandle(Exception exception) => exception is ServiceUnavailableException;

    public ErrorResponse Convert(Exception exception)
    {
        ServiceUnavailableException ex = (ServiceUnavailableException)exception;
        return new ErrorResponse
        {
            StatusCode = ex.StatusCode,
            Message = ex.Message,
            ErrorCode = ex.ErrorCode.ToString(),
#if DEBUG
            Details = exception.ToString()
#endif
        };
    }
}