using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Domain.Services;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions.Interfaces;

public class IdempotencyMatchHandler : BaseExceptionHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public IdempotencyMatchHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception)
    {
        ChartOfAccount? checkAccount = await _repository.GetByCodeAsync(account.Code);

        if (checkAccount != null &&
            checkAccount.IdempotencyKey == account.IdempotencyKey &&
            ContentHashGenerator.ComputeFor(account) == ContentHashGenerator.ComputeFor(checkAccount))
            return true; // Exceção tratada, operação idempotente

        if (NextHandler != null)
            return await NextHandler.HandleAsync(account, exception);

        return false; // Não tratado
    }
}