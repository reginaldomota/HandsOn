using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public class CodeExistsHandler : BaseExceptionHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public CodeExistsHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception)
    {
        ChartOfAccount? checkAccount = await _repository.GetByCodeAsync(account.Code);

        if (checkAccount != null)
        {
            throw new BusinessRuleValidationException(
                string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeAlreadyExists, account.Code), exception);
        }

        if (NextHandler != null)
            return await NextHandler.HandleAsync(account, exception);

        return false; // Não tratado
    }
}