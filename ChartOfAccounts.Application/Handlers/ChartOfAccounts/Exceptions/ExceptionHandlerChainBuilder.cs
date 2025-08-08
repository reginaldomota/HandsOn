using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;

public class ExceptionHandlerChainBuilder : IExceptionHandlerChainBuilder
{
    private readonly IdempotencyMatchHandler _idempotencyMatchHandler;
    private readonly CodeExistsHandler _codeExistsHandler;
    private readonly IdempotencyConflictHandler _idempotencyConflictHandler;
    private readonly DefaultExceptionHandler _defaultExceptionHandler;

    public ExceptionHandlerChainBuilder(
        IdempotencyMatchHandler idempotencyMatchHandler,
        CodeExistsHandler codeExistsHandler,
        IdempotencyConflictHandler idempotencyConflictHandler,
        DefaultExceptionHandler defaultExceptionHandler)
    {
        _idempotencyMatchHandler = idempotencyMatchHandler;
        _codeExistsHandler = codeExistsHandler;
        _idempotencyConflictHandler = idempotencyConflictHandler;
        _defaultExceptionHandler = defaultExceptionHandler;
    }

    public IExceptionHandler Build()
    {
        // Configura a ordem da cadeia de tratamento de exceções
        _idempotencyMatchHandler
            .SetNext(_codeExistsHandler)
            .SetNext(_idempotencyConflictHandler)
            .SetNext(_defaultExceptionHandler);

        return _idempotencyMatchHandler;
    }

    public async Task HandleExceptionAsync(ChartOfAccount account, DataIntegrityViolationException exception)
    {
        IExceptionHandler chain = Build();
        await chain.HandleAsync(account, exception);
    }
}