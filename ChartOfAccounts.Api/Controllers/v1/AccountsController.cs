using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Models;
using ChartOfAccounts.Application.Responses;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts")]
[Tags("Plano de Contas")]
public class AccountsController : ControllerBase
{

    private readonly IChartOfAccountsService _service;

    public AccountsController(IChartOfAccountsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChartOfAccountResponse>>> GetAll()
    {
        IEnumerable<ChartOfAccount> accounts = await _service.GetAllAsync();

        List<ChartOfAccountResponse> response = accounts
            .Select(account => new ChartOfAccountResponse(account))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<ChartOfAccountResponse>> GetByCode(string code)
    {
        ChartOfAccount? account = await _service.GetByCodeAsync(code);

        if (account == null)
            return NotFound();

        return Ok(new ChartOfAccountResponse(account));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChartOfAccountCreateModel model)
    {
        ChartOfAccount account = new ChartOfAccount
        {
            Code = model.Code,
            Name = model.Name,
            Description = model.Description,
            Type = model.Type,
            IsPostable = model.IsPostable,
            ParentCode = model.ParentCode,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _service.AddAsync(account);

        return CreatedAtAction(nameof(GetByCode), new { code = account.Code }, new ChartOfAccountResponse(account));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return NoContent();
    }
}
