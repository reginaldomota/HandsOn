using FluentAssertions;
using FluentAssertions.Primitives;
using System;

namespace ChartOfAccounts.Tests.Shared;

public static class CustomAssertionsExtensions
{
    public static void BeValidChartOfAccount(this ObjectAssertions assertions)
    {
        var account = assertions.Subject;
        
        assertions.NotBeNull();
        
        // Add custom assertions for chart of account validation
        account.GetType().GetProperty("Code").Should().NotBeNull();
        account.GetType().GetProperty("Name").Should().NotBeNull();
        
        var code = account.GetType().GetProperty("Code").GetValue(account);
        var name = account.GetType().GetProperty("Name").GetValue(account);
        
        code.Should().NotBeNull();
        name.Should().NotBeNull();
    }
}