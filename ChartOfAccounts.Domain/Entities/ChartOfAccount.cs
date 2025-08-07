using System.ComponentModel.DataAnnotations;
using ChartOfAccounts.Domain.Interfaces;

namespace ChartOfAccounts.Domain.Entities;

public class ChartOfAccount : ITenantEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A chave de idempotência é obrigatória.")]
    public Guid IdempotencyKey { get; set; }

    [Required(ErrorMessage = "O código é obrigatório.")]
    [StringLength(10, ErrorMessage = "O código deve ter no máximo {1} caracteres.")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(255, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O código normalizado é obrigatório.")]
    [StringLength(1024, ErrorMessage = "O código normalizado deve ter no máximo {1} caracteres.")]
    public string CodeNormalized { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [StringLength(50, ErrorMessage = "O tipo deve ter no máximo {1} caracteres.")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'É Lançável' é obrigatório.")]
    public bool IsPostable { get; set; }

    [StringLength(1024, ErrorMessage = "O código do pai deve ter no máximo {1} caracteres.")]
    public string? ParentCode { get; set; }

    [Required(ErrorMessage = "O ID da requisição é obrigatório.")]
    [StringLength(32, ErrorMessage = "O ID da requisição deve ter no máximo {1} caracteres.")]
    public string RequestId { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID do tenant é obrigatório.")]
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "A data de criação é obrigatória.")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "A data de atualização é obrigatória.")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
