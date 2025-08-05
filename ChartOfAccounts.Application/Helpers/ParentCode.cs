namespace ChartOfAccounts.Application.Helpers;

public static class ParentCode
{
    public static string? GetParentCode(string code)
    {
        var parts = code.Split('.');
        return parts.Length > 1 ? string.Join('.', parts.Take(parts.Length - 1)) : null;
    }
}
