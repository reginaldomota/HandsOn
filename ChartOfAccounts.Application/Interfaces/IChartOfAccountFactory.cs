using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Entities;

namespace ChartOfAccounts.Application.Interfaces;

public interface IChartOfAccountFactory
{
    ChartOfAccount Create(ChartOfAccountCreateDto model, IRequestContext context);
}
