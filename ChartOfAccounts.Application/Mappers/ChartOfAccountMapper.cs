using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.Helpers;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Mappers;

public static class ChartOfAccountMapper
{
    public static ChartOfAccount ToEntity(this ChartOfAccountCreateDto model)
    {
        return new ChartOfAccount
        {
            Code = model.Code,
            Name = model.Name,
            Type = model.Type,
            IsPostable = model.IsPostable,
            CodeNormalized = CodeNormalizer.Normalize(model.Code),
            ParentCode = ParentCode.GetParentCode(model.Code),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
