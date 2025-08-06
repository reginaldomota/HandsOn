using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Application.Errors.Converters;

public class DefaultExceptionConverter : IErrorResponseConverter
{
    public bool CanHandle(Exception exception) => true;

    public ErrorResponse Convert(Exception exception)
    {
        return new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.InternalServerError.ToString(),
            Message = "Ocorreu um erro inesperado.",
#if DEBUG
            Details = exception.ToString()
#endif
        };
    }
}
