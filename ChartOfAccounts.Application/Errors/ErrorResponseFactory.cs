using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Errors.Converters;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Application.Errors.Interfaces;

namespace ChartOfAccounts.Application.Errors;

public class ErrorResponseFactory : IErrorResponseFactory
{
    private readonly IEnumerable<IErrorResponseConverter> _converters;

    public ErrorResponseFactory(IEnumerable<IErrorResponseConverter> converters)
    {
        _converters = converters;
    }

    public ErrorResponse Create(Exception exception)
    {
        IErrorResponseConverter converter = _converters.First(c => c.CanHandle(exception));
        return converter.Convert(exception);
    }
}
