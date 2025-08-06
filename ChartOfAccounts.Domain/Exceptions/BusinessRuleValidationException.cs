using ChartOfAccounts.Domain.Enums;
using System.Net;

namespace ChartOfAccounts.Domain.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public int StatusCode => (int)HttpStatusCode.UnprocessableEntity;
    public ErrorCode ErrorCode => ErrorCode.BusinessRuleViolation;

    public BusinessRuleValidationException()
        : base("A operação viola uma regra de negócio.") { }

    public BusinessRuleValidationException(string message)
        : base(message) { }

    public BusinessRuleValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}
