using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.DTOs.ChartOfAccounts;

public class ChartOfAccountResponseDto
{
    public ChartOfAccountResponseDto(ChartOfAccount entity)
    {
        Code = entity.Code;
        Name = entity.Name;
        Type = entity.Type;
        IsPostable = entity.IsPostable!.Value;
        ParentCode = entity.ParentCode;
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsPostable { get; set; }
    public string? ParentCode { get; set; }
}
