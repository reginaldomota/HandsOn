using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;

namespace ChartOfAccounts.Application.Handlers.ChartOfAccounts;

public class DeleteValidationChainBuilder : IDeleteValidationChainBuilder
{
    private readonly IDeleteValidationHandler _childrenValidationHandler;

    public DeleteValidationChainBuilder(IDeleteValidationHandler childrenValidationHandler)
    {
        _childrenValidationHandler = childrenValidationHandler;
    }

    public IDeleteValidationHandler Build()
    {
        // Aqui você configura a ordem da cadeia de validação
        // Atualmente só temos um handler, mas quando adicionar mais, é aqui que você define a ordem
        
        // Retorna o primeiro validador da cadeia
        return _childrenValidationHandler;
    }

    public async Task ValidateAsync(string code)
    {
        IDeleteValidationHandler validationChain = Build();
        await validationChain.ValidateAsync(code);
    }
}