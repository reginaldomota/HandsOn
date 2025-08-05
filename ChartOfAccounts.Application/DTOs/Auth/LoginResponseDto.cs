namespace ChartOfAccounts.Application.DTOs.Auth;

public record LoginResponseDto
{
    public string AccessToken { get; init; } = string.Empty;
    public string TokenType { get; init; } = "Bearer";
    public long Exp { get; init; }
}
