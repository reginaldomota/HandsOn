using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
    private readonly IChartOfAccountFactory _chartOfAccount;

    public AccountsController(IChartOfAccountsService service, IRequestContext requestContext, IChartOfAccountFactory chartOfAccount)
    {
        _service = service;
        _requestContext = requestContext;
        _chartOfAccount = chartOfAccount;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResultDto<ChartOfAccountResponseDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 100,
        [FromQuery] string? find = null,
        [FromQuery] bool? isPostable = null)
    {
        PaginatedResultDto<ChartOfAccount> paged = await _service.GetPagedAsync(page, pageSize, find, isPostable);

        PaginatedResultDto<ChartOfAccountResponseDto> response = new PaginatedResultDto<ChartOfAccountResponseDto>
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
    public async Task<IActionResult> Create([FromBody] ChartOfAccountCreateDto model)
    {
        ChartOfAccount account = _chartOfAccount.Create(model, _requestContext);

        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(account, new ValidationContext(account), validationResults, validateAllProperties: true);

        if (!isValid)
        {
            var errors = validationResults
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(r => r.ErrorMessage).ToArray()
                );

            return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorCode = ErrorCode.ValidationError.ToString(),
                    Message = string.Format(ErrorMessages.Error_ChartOfAccounts_ValidationFailed),
                    Detail = errors
            });
        }

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
