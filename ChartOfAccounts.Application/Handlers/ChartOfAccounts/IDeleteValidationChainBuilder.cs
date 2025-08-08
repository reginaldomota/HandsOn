using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public interface IDeleteValidationChainBuilder
{
    IDeleteValidationHandler Build();
    Task ValidateAsync(string code);
}