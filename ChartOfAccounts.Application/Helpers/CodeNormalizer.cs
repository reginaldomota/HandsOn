namespace ChartOfAccounts.Application.Helpers;

public static class CodeNormalizer
{
    public static string Normalize(string code, int padLength = 3)
    {
        return string.Join(".",
            code.Split('.')
                .Select(part => int.Parse(part).ToString().PadLeft(padLength, '0')));
    }
}
