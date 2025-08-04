using ChartOfAccounts.Application.Interfaces;
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
        return await FindNextCodeRecursiveAsync(parentCode, 1);
    }

    private async Task<string?> FindNextCodeRecursiveAsync(string parentCode, int level)
    {
        if (level > MaxLevel)
            return null; // proteção contra recursão infinita

        var childrenCodes = await _repository.GetChildrenCodesAsync(parentCode);

        // Pega todos os sufixos numéricos diretos (ex: "1.2.5" → 5)
        var childNumbers = childrenCodes
            .Select(code => GetChildSuffixNumber(parentCode, code))
            .Where(n => n != null)
            .Select(n => n!.Value)
            .ToList();

        var nextNumber = childNumbers.Any() ? childNumbers.Max() + 1 : 1;

        if (nextNumber <= MaxChildren)
        {
            return $"{parentCode}.{nextNumber}";
        }

        // Limite atingido, tentar recursivamente em um novo parent
        var newParent = GetNewParentCode(parentCode);
        if (newParent == null)
            return null;

        return await FindNextCodeRecursiveAsync(newParent, level + 1);
    }

    private int? GetChildSuffixNumber(string parentCode, string childCode)
    {
        if (!childCode.StartsWith(parentCode + ".")) return null;

        var suffix = childCode.Substring(parentCode.Length + 1); // remove prefixo + ponto
        var nextPart = suffix.Split('.').First(); // só a parte imediatamente abaixo

        return int.TryParse(nextPart, out int value) ? value : null;
    }

    private string? GetNewParentCode(string currentCode)
    {
        var parts = currentCode.Split('.').ToList();

        for (int i = parts.Count - 1; i >= 0; i--)
        {
            if (int.TryParse(parts[i], out int number) && number < MaxChildren)
            {
                parts[i] = (number + 1).ToString();
                return string.Join('.', parts.Take(i + 1));
            }
        }

        return null;
    }
}
