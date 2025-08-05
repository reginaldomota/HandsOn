using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Domain.Exceptions;

public class ServiceUnavailableException : Exception
{
    public int StatusCode => (int)HttpStatusCode.ServiceUnavailable;
    public ErrorCode ErrorCode => ErrorCode.ServiceUnavailable;
    public string? RequestIdentifier { get; init; }

    public ServiceUnavailableException()
        : base("Serviço temporariamente indisponível.") { }

    public ServiceUnavailableException(string message, string requestIdentifier)
        : base(message)
    {
        RequestIdentifier = requestIdentifier;
    }

    public ServiceUnavailableException(string message, Exception innerException, string requestIdentifier)
        : base(message, innerException)
    {
        RequestIdentifier = requestIdentifier;
    }
}
