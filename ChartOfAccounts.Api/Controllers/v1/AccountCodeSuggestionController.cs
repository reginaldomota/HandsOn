using ChartOfAccounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts/suggestion")]
[Tags("Plano de Contas")]
public class AccountCodeSuggestionController : ControllerBase
{
    private readonly IAccountCodeSuggestionService _suggestionService;

    public AccountCodeSuggestionController(IAccountCodeSuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    [HttpGet("{parentCode}")]
    public async Task<ActionResult<string>> SuggestNextCode(string parentCode)
    {
        string? suggestion = await _suggestionService.SuggestNextCodeAsync(parentCode);

        if (string.IsNullOrWhiteSpace(suggestion))
            return NotFound("Não foi possível sugerir um próximo código.");

        return Ok(suggestion);
    }
}
