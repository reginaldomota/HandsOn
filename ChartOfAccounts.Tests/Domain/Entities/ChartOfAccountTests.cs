using ChartOfAccounts.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace ChartOfAccounts.Tests.Domain.Entities;

[TestFixture]
public class ChartOfAccountTests
{
    [Test]
    public void ChartOfAccount_InitializedWithDefaultValues()
    {
        // Arrange & Act
        var account = new ChartOfAccount();

        // Assert
        account.Code.Should().BeEmpty();
        account.Name.Should().BeEmpty();
        account.CodeNormalized.Should().BeEmpty();
        account.Type.Should().BeEmpty();
        account.RequestId.Should().BeEmpty();
        account.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        account.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    [Test]
    public void ChartOfAccount_PropertiesSetCorrectly()
    {
        // Arrange
        var code = "1.01.01";
        var name = "Teste";
        var type = "ATIVO";
        var isPostable = true;
        var tenantId = Guid.NewGuid();
        var idempotencyKey = Guid.NewGuid();

        // Act
        var account = new ChartOfAccount
        {
            Code = code,
            Name = name,
            Type = type,
            IsPostable = isPostable,
            TenantId = tenantId,
            IdempotencyKey = idempotencyKey
        };

        // Assert
        account.Code.Should().Be(code);
        account.Name.Should().Be(name);
        account.Type.Should().Be(type);
        account.IsPostable.Should().Be(isPostable);
        account.TenantId.Should().Be(tenantId);
        account.IdempotencyKey.Should().Be(idempotencyKey);
    }
}