using ChartOfAccounts.Application.Helpers;
using ChartOfAccounts.Application.Models;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Mappers;

public static class ChartOfAccountMapper
{
    public static ChartOfAccount ToEntity(this ChartOfAccountCreateModel model)
    {
        return new ChartOfAccount
        {
            Code = model.Code,
            Name = model.Name,
            Type = model.Type,
            IsPostable = model.IsPostable,
            CodeNormalized = CodeNormalizer.Normalize(model.Code),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
