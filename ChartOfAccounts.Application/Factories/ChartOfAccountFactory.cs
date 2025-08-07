using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.Helpers;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Factories;

public class ChartOfAccountFactory : IChartOfAccountFactory
{
    public ChartOfAccount Create(ChartOfAccountCreateDto model, IRequestContext context)
    {
        return new ChartOfAccount
        {
            Code = model.Code,
            Name = model.Name,
            Type = model.Type,
            IsPostable = model.IsPostable,
            CodeNormalized = CodeNormalizer.Normalize(model.Code),
            ParentCode = model.ParentCode,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IdempotencyKey = context.IdempotencyKey,
            RequestId = context.RequestId,
            TenantId = context.TenantId!.Value
        };
    }
}
