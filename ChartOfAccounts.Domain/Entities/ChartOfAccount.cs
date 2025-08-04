namespace ChartOfAccounts.Domain.Entities;

public class ChartOfAccount
{
    public int Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Type { get; set; } = default!;
    public bool IsPostable { get; set; }
    public string? ParentCode { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculado no banco com base na quantidade de níveis no código (ex: 1.1.2 → Level 3)
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// Regras de domínio: só pode ter filhos se não for lançável
    /// </summary>
    public bool CanHaveChildren => !IsPostable;
}
