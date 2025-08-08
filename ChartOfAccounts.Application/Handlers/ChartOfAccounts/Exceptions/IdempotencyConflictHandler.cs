using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public class IdempotencyConflictHandler : BaseExceptionHandler
{
    private readonly IChartOfAccountsRepository _repository;

    public IdempotencyConflictHandler(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public override async Task<bool> HandleAsync(ChartOfAccount account, DataIntegrityViolationException exception)
    {
        ChartOfAccount? checkAccount = await _repository.GetByIdempotencyKeyAsync(account.IdempotencyKey!.Value);

        if (checkAccount != null)
        {
            throw new ErrorHttpRequestException(
                string.Format(ErrorMessages.Error_ChartOfAccounts_IdempotencyConflict, account.IdempotencyKey),
                exception, (int)HttpStatusCode.Conflict, ErrorCode.Conflict);
        }

        if (NextHandler != null)
            return await NextHandler.HandleAsync(account, exception);

        return false; // Não tratado
    }
}