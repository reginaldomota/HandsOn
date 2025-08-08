using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;

public interface ICreateValidationHandler
{
    ICreateValidationHandler? SetNext(ICreateValidationHandler handler);
    Task ValidateAsync(ChartOfAccount account);
}