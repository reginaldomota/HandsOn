using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Domain.Exceptions;

namespace ChartOfAccounts.Application.Errors.Converters;

public class ErrorHttpRequestConverter : IErrorResponseConverter
{
    public bool CanHandle(Exception exception) => exception is ErrorHttpRequestException;

    public ErrorResponse Convert(Exception exception)
    {
        ErrorHttpRequestException ex = (ErrorHttpRequestException)exception;

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
