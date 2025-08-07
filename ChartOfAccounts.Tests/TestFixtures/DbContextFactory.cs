using ChartOfAccounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChartOfAccounts.Tests.TestFixtures;

public static class DbContextFactory
{
    public static AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"ChartOfAccountsTest_{Guid.NewGuid()}")
            .Options;

        // Substitua DbContext pelo seu contexto real
        return new AppDbContext(options);
    }
}