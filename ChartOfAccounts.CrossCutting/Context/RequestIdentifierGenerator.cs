using System.Security.Cryptography;

namespace ChartOfAccounts.CrossCutting.Context;

public static class RequestIdentifierGenerator
{
    public static string Generate()
    {
        string prefix = "RQI";
        string timestamp = DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmss'Z'"); // Ex: 20250805T143312Z
        byte[] randomBytes = RandomNumberGenerator.GetBytes(4); // 32 bits
        string hash = Convert.ToHexString(randomBytes); // Ex: 5A7F3D1C

        return $"{prefix}-{timestamp}-{hash}";
    }
}
