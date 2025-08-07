using System.ComponentModel.DataAnnotations;

namespace ChartOfAccounts.Application.DTOs.ChartOfAccounts;

public record ChartOfAccountCreateDto
{
    public string? Code { get; set; } = default!;

    public string? ParentCode { get; set; } = default!;

    public string? Name { get; set; } = default!;

    public string? Type { get; set; } = default!;

    public bool? IsPostable { get; set; }
}
