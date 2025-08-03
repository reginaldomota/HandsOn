using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{code}")]
    public string Get(string code)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpDelete("{code}")]
    public void Delete(string code)
    {
    }
}
