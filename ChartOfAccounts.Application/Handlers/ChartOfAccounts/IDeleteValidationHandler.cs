namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public interface IDeleteValidationHandler
{
    IDeleteValidationHandler? SetNext(IDeleteValidationHandler handler);
    Task ValidateAsync(string code);
}