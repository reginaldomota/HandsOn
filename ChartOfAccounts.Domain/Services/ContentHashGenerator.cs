using ChartOfAccounts.Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ChartOfAccounts.Domain.Services;

public static class ContentHashGenerator
{
    public static string ComputeFor(ChartOfAccount account)
    {
        var data = new
        {
            account.Code,
            account.Name,
            account.Type,
            account.IsPostable,
            account.TenantId
        };

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        using SHA512 sha = SHA512.Create();
        byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToHexString(hashBytes);
    }
}

