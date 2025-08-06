using System.ComponentModel.DataAnnotations;

namespace ChartOfAccounts.Application.DTOs.ChartOfAccounts;

public record ChartOfAccountCreateDto
{
    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Type { get; set; } = default!;

    public bool IsPostable { get; set; }
}
