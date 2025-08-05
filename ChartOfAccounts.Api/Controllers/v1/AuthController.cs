using ChartOfAccounts.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChartOfAccounts.Api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/auth")]
[Tags("Autenticação JWT")]
public class AuthController : ControllerBase
{
    private const string SecretKey = "d8afacb3-bf57-45ae-a6c3-174e8422ee78";
    private const string Issuer = "ChartOfAccounts.Auth";
    private const string Audience = "ChartOfAccounts.ApiClients";

    private readonly Dictionary<string, (string Password, string TenantId)> _users = new()
    {
        ["tenant_1"] = ("123", "6b9a29df-482c-4b3e-9c38-032f3a39f45f"),
        ["tenant_2"] = ("123", "c3c12e1a-7b96-4a2a-b21a-f0f9a95d8d4c"),
        ["tenant_3"] = ("123", "44e0a5cf-8cf6-4b78-b49d-abc1df098118")
    };

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto login)
    {
        if (!_users.TryGetValue(login.Username, out var userInfo)
                || userInfo.Password != login.Password
                || userInfo.TenantId != login.TenantId)
            return Unauthorized("Credenciais inválidas");

        DateTime expiresAt = DateTime.UtcNow.AddHours(1);
        long expUnix = EpochTime.GetIntDate(expiresAt);

        string token = GenerateJwtToken(login.Username, login.TenantId, expUnix);

        return Ok(new LoginResponseDto
        {
            AccessToken = token,
            Exp = expUnix
        });
    }

    private string GenerateJwtToken(string username, string tenantId, long expUnix)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        DateTime issuedAt = DateTime.UtcNow;
        DateTime expiresAt = EpochTime.DateTime(expUnix);

        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim("tenant", tenantId),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, Audience),
            new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(issuedAt).ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Nbf, EpochTime.GetIntDate(issuedAt).ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Exp, expUnix.ToString(), ClaimValueTypes.Integer64)
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: Issuer,
            claims: claims,
            notBefore: issuedAt,
            expires: expiresAt,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string jwt = handler.WriteToken(token);

        return jwt;
    }
}
