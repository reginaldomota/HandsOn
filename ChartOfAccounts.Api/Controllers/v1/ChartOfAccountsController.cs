using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ChartOfAccountsController : ControllerBase
{
    // GET: api/<ChartOfAccountsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<ChartOfAccountsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ChartOfAccountsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ChartOfAccountsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ChartOfAccountsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
