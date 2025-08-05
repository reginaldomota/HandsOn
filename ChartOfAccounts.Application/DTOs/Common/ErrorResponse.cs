using ChartOfAccounts.Domain.Enums;

namespace ChartOfAccounts.Application.DTOs.Common;

public record ErrorResponse
{
    public int StatusCode { get; init; }
    public string? ErrorCode { get; init; }
    public string? Message { get; init; } = null;
    public string? RequestIdentifier { get; init; } = null;
    public string? Details { get; init; } = null;
}
