using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Responses;

public class ChartOfAccountResponse
{
    public ChartOfAccountResponse(ChartOfAccount entity)
    {
        Code = entity.Code;
        Name = entity.Name;
        Description = entity.Description;
        Type = entity.Type;
        IsPostable = entity.IsPostable;
        ParentCode = entity.ParentCode;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt;
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public bool IsPostable { get; set; }
    public string? ParentCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
