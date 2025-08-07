using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts/suggestion")]
[Tags("Plano de Contas")]
[Authorize]
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
            return NotFound(new
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorCode = ErrorCode.NotFound.ToString(),
                Message = ErrorMessages.Error_ChartOfAccounts_Suggestion_LimitReached
            });

        return Ok(new AccountCodeSuggestionResponseDto { SuggestedCode = suggestion }); 
    }
}
