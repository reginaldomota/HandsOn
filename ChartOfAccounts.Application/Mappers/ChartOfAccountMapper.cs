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
            Description = model.Description,
            Type = model.Type,
            IsPostable = model.IsPostable,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
