using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Application.Services;

public class AccountCodeSuggestionService : IAccountCodeSuggestionService
{
    private readonly IChartOfAccountsRepository _repository;
    private const int MaxLevel = 5;
    private const int MaxChildren = 999;

    public AccountCodeSuggestionService(IChartOfAccountsRepository repository)
    {
        _repository = repository;
    }

    public async Task<string?> SuggestNextCodeAsync(string parentCode)
    {
        bool? isPostable = await _repository.IsPostableAsync(parentCode);
        
        if (isPostable is null)
            throw new InvalidOperationException($"O código {parentCode} não existe ou não é um código pai válido."); //todo criar excessao customizada e consumir no middleware. criar resource

        if (isPostable == true)
            throw new InvalidOperationException($"O código {parentCode} não aceita contas filhas pois ele permite lançamentos."); //todo criar excessao customizada e consumir no middleware

        var parentLevel = parentCode.Split('.').Length;
        return await SuggestNextCodeRecursiveAsync(parentCode, parentLevel);
    }

    private async Task<string?> SuggestNextCodeRecursiveAsync(string currentCode, int level)
    {
        if (level >= MaxLevel)
            return null;

        var childCodes = await _repository.GetChildrenCodesAsync(currentCode);
        var nextChild = GetNextSuffix(childCodes, currentCode);

        if (nextChild <= MaxChildren)
            return $"{currentCode}.{nextChild}";

        var parentCode = GetParentCode(currentCode);
        if (parentCode == null)
            return null;

        var parentLevel = parentCode.Split('.').Length;
        return await SuggestNextCodeRecursiveAsync(parentCode, parentLevel);
    }

    private int GetNextSuffix(IEnumerable<string> codes, string baseCode)
    {
        return codes
            .Select(code => GetChildSuffixNumber(baseCode, code))
            .Where(n => n != null)
            .Select(n => n!.Value)
            .DefaultIfEmpty(0)
            .Max() + 1;
    }

    private string? GetParentCode(string code)
    {
        var parts = code.Split('.');
        return parts.Length > 1
            ? string.Join('.', parts.Take(parts.Length - 1))
            : null;
    }

    private int? GetChildSuffixNumber(string parentCode, string childCode)
    {
        if (!childCode.StartsWith(parentCode + ".")) return null;

        var suffix = childCode.Substring(parentCode.Length + 1);
        var nextPart = suffix.Split('.').First();

        return int.TryParse(nextPart, out int value) ? value : null;
    }
}
