using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/accounts")]
[Tags("Plano de Contas")]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetAllAccounts()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{code}")]
    public string GetAccountByCode(string code)
    {
        return "value";
    }

    [HttpPost]
    public void CreateAccount([FromBody] string value)
    {
    }

    [HttpDelete("{code}")]
    public void DeleteAccount(string code)
    {
    }
}
