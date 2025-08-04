using System.ComponentModel.DataAnnotations;

namespace ChartOfAccounts.Application.Models;

public class ChartOfAccountCreateModel
{
    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public string Type { get; set; } = default!;

    public bool IsPostable { get; set; }

    public string? ParentCode { get; set; }
}
