namespace ChartOfAccounts.Application.DTOs;

public record LoginResponseDto
{
    public string AccessToken { get; init; } = string.Empty;
    public string TokenType { get; init; } = "Bearer";
    public long Exp { get; init; }
}
