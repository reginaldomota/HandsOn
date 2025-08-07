using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts")]
[Tags("Plano de Contas")]
[Authorize]
public class AccountsController : ControllerBase
{

    private readonly IChartOfAccountsService _service;
    private readonly IRequestContext _requestContext;

    public AccountsController(IChartOfAccountsService service, IRequestContext requestContext)
    {
        _service = service;
        _requestContext = requestContext;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResultDto<ChartOfAccountResponseDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var paged = await _service.GetPagedAsync(page, pageSize);

        var response = new PaginatedResultDto<ChartOfAccountResponseDto>
        {
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount,
            Items = paged.Items.Select(a => new ChartOfAccountResponseDto(a))
        };

        return Ok(response);
    }


    [HttpGet("{code}")]
    public async Task<ActionResult<ChartOfAccountResponseDto>> GetByCode(string code)
    {
        ChartOfAccount? account = await _service.GetByCodeAsync(code);

        if (account == null)
            return NotFound(new
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorCode = ErrorCode.NotFound.ToString(),
                Message = string.Format(ErrorMessages.Error_ChartOfAccounts_NotFound, code)
            });

        return Ok(new ChartOfAccountResponseDto(account));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChartOfAccountCreateDto model, IChartOfAccountFactory chartOfAccount)
    {
        ChartOfAccount account = chartOfAccount.Create(model, _requestContext);

        await _service.CreateAsync(account);

        return CreatedAtAction(nameof(GetByCode), new { code = account.Code }, new ChartOfAccountResponseDto(account));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return NoContent();
    }
}
