using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts/suggestion")]
[Tags("Plano de Contas")]
public class AccountCodeSuggestionController : ControllerBase
{
    [HttpGet("{parentCode}")]
    public string SuggestNextCode(string parentCode)
    {
        return "value";
    }
}
