namespace ChartOfAccounts.Application.DTOs.Auth;

public record LoginRequestDto
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string TenantId { get; init; } = string.Empty;
}
