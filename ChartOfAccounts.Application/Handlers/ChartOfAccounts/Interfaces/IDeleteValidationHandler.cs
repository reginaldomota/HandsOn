namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;

public interface IDeleteValidationHandler
{
    IDeleteValidationHandler? SetNext(IDeleteValidationHandler handler);
    Task ValidateAsync(string code);
}