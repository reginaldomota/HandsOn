using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Responses;

public class ChartOfAccountResponse
{
    public ChartOfAccountResponse(ChartOfAccount entity)
    {
        Code = entity.Code;
        Name = entity.Name;
        Type = entity.Type;
        IsPostable = entity.IsPostable;
        ParentCode = entity.ParentCode;
        Level = entity.Level;
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsPostable { get; set; }
    public string? ParentCode { get; set; }
    public int Level { get; private set; }
}
