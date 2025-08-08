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
        // Aqui voc� configura a ordem da cadeia de valida��o
        // Atualmente s� temos um handler, mas quando adicionar mais, � aqui que voc� define a ordem
        
        // Retorna o primeiro validador da cadeia
        return _childrenValidationHandler;
    }

    public async Task ValidateAsync(string code)
    {
        IDeleteValidationHandler validationChain = Build();
        await validationChain.ValidateAsync(code);
    }
}