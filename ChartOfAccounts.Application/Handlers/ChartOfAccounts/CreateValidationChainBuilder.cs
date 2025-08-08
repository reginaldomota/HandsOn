using ChartOfAccounts.Domain.Entities;
using System.Threading.Tasks;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class CreateValidationChainBuilder : ICreateValidationChainBuilder
{
    private readonly MaxLevelValidationHandler _maxLevelHandler;
    private readonly ParentCodeValidationHandler _parentCodeHandler;
    private readonly ParentExistenceValidationHandler _parentExistenceHandler;
    private readonly ParentPostableValidationHandler _parentPostableHandler;
    private readonly ParentTypeValidationHandler _parentTypeHandler;

    public CreateValidationChainBuilder(
        MaxLevelValidationHandler maxLevelHandler,
        ParentCodeValidationHandler parentCodeHandler,
        ParentExistenceValidationHandler parentExistenceHandler,
        ParentPostableValidationHandler parentPostableHandler,
        ParentTypeValidationHandler parentTypeHandler)
    {
        _maxLevelHandler = maxLevelHandler;
        _parentCodeHandler = parentCodeHandler;
        _parentExistenceHandler = parentExistenceHandler;
        _parentPostableHandler = parentPostableHandler;
        _parentTypeHandler = parentTypeHandler;
    }

    public ICreateValidationHandler Build()
    {
        // Configure a ordem da cadeia de validação
        _maxLevelHandler
            .SetNext(_parentCodeHandler)
            .SetNext(_parentExistenceHandler)
            .SetNext(_parentPostableHandler)
            .SetNext(_parentTypeHandler);

        return _maxLevelHandler;
    }

    public async Task ValidateAsync(ChartOfAccount account)
    {
        ICreateValidationHandler chain = Build();
        await chain.ValidateAsync(account);
    }
}