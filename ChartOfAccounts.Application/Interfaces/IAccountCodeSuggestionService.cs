namespace ChartOfAccounts.Application.Interfaces;

public interface IAccountCodeSuggestionService
{
    Task<(string? SuggestedCode, string? Type)> SuggestNextCodeAsync(string parentCode);
}
