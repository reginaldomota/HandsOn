using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;

public interface IDeleteValidationChainBuilder
{
    IDeleteValidationHandler Build();
    Task ValidateAsync(string code);
}