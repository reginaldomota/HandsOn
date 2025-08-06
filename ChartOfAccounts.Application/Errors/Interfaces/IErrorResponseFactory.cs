using ChartOfAccounts.Application.DTOs.Common;

namespace ChartOfAccounts.Application.Errors.Interfaces;

public interface IErrorResponseFactory
{
    ErrorResponse Create(Exception exception);
}
