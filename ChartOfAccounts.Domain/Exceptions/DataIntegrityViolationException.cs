using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Domain.Exceptions;

public class DataIntegrityViolationException : Exception
{
    public int StatusCode => (int)HttpStatusCode.Conflict;
    public ErrorCode ErrorCode => ErrorCode.DataIntegrityViolation;
    public string? RequestIdentifier { get; init; }

    public DataIntegrityViolationException(string message, string? requestIdentifier = null)
        : base(message)
    {
        RequestIdentifier = requestIdentifier;
    }

    public DataIntegrityViolationException(string message, Exception innerException, string? requestIdentifier = null)
        : base(message, innerException)
    {
        RequestIdentifier = requestIdentifier;
    }
}
