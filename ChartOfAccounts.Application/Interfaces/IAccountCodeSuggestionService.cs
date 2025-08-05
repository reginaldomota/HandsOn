namespace ChartOfAccounts.Application.Interfaces;

public interface IAccountCodeSuggestionService
{
    Task<string?> SuggestNextCodeAsync(string parentCode);
}
