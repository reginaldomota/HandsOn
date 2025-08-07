using ChartOfAccounts.Application.DTOs;
using ChartOfAccounts.Application.DTOs.Auth;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Enums;
using ChartOfAccounts.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
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
        ["tenant_1"] = ("secret1", "5e0d1c8a-1000-4000-b000-000000000001"),
        ["tenant_2"] = ("secret2", "5e0d1c8a-1000-4000-b000-000000000002"),
        ["tenant_3"] = ("secret3", "5e0d1c8a-1000-4000-b000-000000000003")
    };

    [HttpPost("token")]
    public IActionResult Login([FromBody] LoginRequestDto login)
    {
        if (!_users.TryGetValue(login.Username, out var userInfo)
                || userInfo.Password != login.Password
                || userInfo.TenantId != login.TenantId)
            return Unauthorized(new
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    ErrorCode = ErrorCode.Unauthorized.ToString(),
                    Message = ErrorMessages.Error_Auth_InvalidCredentials
                });
                
               
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
