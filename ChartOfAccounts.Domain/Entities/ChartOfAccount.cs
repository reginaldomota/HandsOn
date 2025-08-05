namespace ChartOfAccounts.Domain.Entities;

public class ChartOfAccount
{
    public int Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
    public bool IsPostable { get; set; }
    public string? ParentCode { get; set; }
    public int Level { get; private set; } 
    public bool CanHaveChildren => !IsPostable;
    public string CodeNormalized { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
