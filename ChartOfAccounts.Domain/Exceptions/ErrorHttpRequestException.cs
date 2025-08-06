using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Domain.Exceptions;

public class ErrorHttpRequestException : Exception
{
    public int StatusCode { get; } = (int)HttpStatusCode.InternalServerError;
    public ErrorCode ErrorCode { get; } = ErrorCode.InternalServerError;

    public ErrorHttpRequestException()
        : base("Houve um erro inesperado") { }

    public ErrorHttpRequestException(string message, int statusCode, ErrorCode errorCode)
        : base(message) 
    { 
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    public ErrorHttpRequestException(string message, Exception innerException, int statusCode, ErrorCode errorCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
