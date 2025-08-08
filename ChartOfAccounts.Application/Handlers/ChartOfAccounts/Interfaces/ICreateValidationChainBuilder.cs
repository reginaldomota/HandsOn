using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;

public interface ICreateValidationChainBuilder
{
    ICreateValidationHandler Build();
    Task ValidateAsync(ChartOfAccount account);
}