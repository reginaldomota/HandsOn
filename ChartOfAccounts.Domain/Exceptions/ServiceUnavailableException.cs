using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Domain.Exceptions;

public class ServiceUnavailableException : Exception
{
    public int StatusCode => (int)HttpStatusCode.ServiceUnavailable;
    public ErrorCode ErrorCode => ErrorCode.ServiceUnavailable;

    public ServiceUnavailableException()
        : base("Serviço temporariamente indisponível.") { }

    public ServiceUnavailableException(string message)
        : base(message) { }

    public ServiceUnavailableException(string message, Exception innerException)
        : base(message, innerException) { }
}
