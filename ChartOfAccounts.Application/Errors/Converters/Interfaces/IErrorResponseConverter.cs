using ChartOfAccounts.Application.DTOs.Common;

namespace ChartOfAccounts.Application.Errors.Converters.Interfaces;

public interface IErrorResponseConverter
{
    bool CanHandle(Exception exception);
    ErrorResponse Convert(Exception exception);
}
