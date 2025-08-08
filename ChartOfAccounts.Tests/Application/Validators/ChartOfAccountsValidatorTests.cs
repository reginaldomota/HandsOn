using ChartOfAccounts.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace ChartOfAccounts.Tests.Application.Validators;

[TestFixture]
public class ChartOfAccountsValidatorTests
{
    [Test]
    public void Validate_ValidAccount_ShouldNotThrowException()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            Code = "1.01.01",
            Name = "Teste",
            IsPostable = true,
            Type = "ATIVO",
            CodeNormalized = "1.01.01",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        // Act & Assert
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(account, new ValidationContext(account), validationResults, true);
        
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Test]
    public void Validate_MissingRequiredFields_ShouldFailValidation()
    {
        // Arrange
        var account = new ChartOfAccount();

        // Act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(account, new ValidationContext(account), validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().NotBeEmpty();
        validationResults.Select(vr => vr.MemberNames.First()).Should().Contain(nameof(ChartOfAccount.Code));
        validationResults.Select(vr => vr.MemberNames.First()).Should().Contain(nameof(ChartOfAccount.Name));
    }

    [Test]
    public void Validate_CodeTooLong_ShouldFailValidation()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            Code = new string('1', 1025), // Code is limited to 1024 chars
            Name = "Teste",
            IsPostable = true,
            Type = "ATIVO",
            CodeNormalized = "1.01.01",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(account, new ValidationContext(account), validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(vr => vr.MemberNames.Contains(nameof(ChartOfAccount.Code)));
    }
}