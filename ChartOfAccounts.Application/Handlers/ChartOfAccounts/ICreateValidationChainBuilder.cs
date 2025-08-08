using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public interface ICreateValidationChainBuilder
{
    ICreateValidationHandler Build();
    Task ValidateAsync(ChartOfAccount account);
}