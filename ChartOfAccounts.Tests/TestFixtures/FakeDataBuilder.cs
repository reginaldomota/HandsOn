using ChartOfAccounts.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ChartOfAccounts.Tests.TestFixtures;

public static class FakeDataBuilder
{
    public static ChartOfAccount CreateChartOfAccount(
        string code = "1.01.01", 
        string name = "Test Account", 
        bool isPostable = true)
    {
        return new ChartOfAccount
        {
            Code = code,
            Name = name,
            IsPostable = isPostable,
            Type = "ATIVO",
            CodeNormalized = code,
            RequestId = Guid.NewGuid().ToString().Substring(0, 8),
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static List<ChartOfAccount> CreateManyChartOfAccounts(int count = 10)
    {
        var accounts = new List<ChartOfAccount>();
        
        for (int i = 1; i <= count; i++)
        {
            accounts.Add(CreateChartOfAccount(
                code: $"1.{i:00}.{i:00}",
                name: $"Test Account {i}",
                isPostable: i % 2 == 0
            ));
        }
        
        return accounts;
    }
}