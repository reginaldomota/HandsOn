using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public interface ICreateValidationHandler
{
    ICreateValidationHandler? SetNext(ICreateValidationHandler handler);
    Task ValidateAsync(ChartOfAccount account);
}