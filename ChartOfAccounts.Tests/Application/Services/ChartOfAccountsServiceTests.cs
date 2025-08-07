using ChartOfAccounts.Application.Services;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Domain.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace ChartOfAccounts.Tests.Application.Services;

[TestFixture]
public class ChartOfAccountsServiceTests
{
    private Mock<IChartOfAccountsRepository> _repositoryMock;
    private ChartOfAccountsService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IChartOfAccountsRepository>();
        _service = new ChartOfAccountsService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetByCodeAsync_WhenCodeExists_ShouldReturnAccount()
    {
        // Arrange
        var code = "1.01.01";
        var expectedAccount = new ChartOfAccount
        {
            Code = code,
            Name = "Teste",
            IsPostable = true,
            Type = "ATIVO",
            CodeNormalized = "1.01.01",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        _repositoryMock.Setup(repo => repo.GetByCodeAsync(code))
            .ReturnsAsync(expectedAccount);

        // Act
        var result = await _service.GetByCodeAsync(code);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAccount);
        _repositoryMock.Verify(repo => repo.GetByCodeAsync(code), Times.Once);
    }

    [Test]
    public async Task GetByCodeAsync_WhenCodeDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var nonExistentCode = "9.99.99";
        _repositoryMock.Setup(repo => repo.GetByCodeAsync(nonExistentCode))
            .ReturnsAsync((ChartOfAccount)null);

        // Act
        var result = await _service.GetByCodeAsync(nonExistentCode);

        // Assert
        result.Should().BeNull();
        _repositoryMock.Verify(repo => repo.GetByCodeAsync(nonExistentCode), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WithValidAccount_ShouldCallRepository()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            Code = "1.01.02",
            Name = "Teste",
            IsPostable = false,
            Type = "ATIVO",
            CodeNormalized = "1.01.02",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        _repositoryMock.Setup(repo => repo.IsPostableAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        _repositoryMock.Setup(repo => repo.CreateAsync(account))
            .Returns(Task.CompletedTask);

        // Act
        await _service.CreateAsync(account);

        // Assert
        _repositoryMock.Verify(repo => repo.CreateAsync(account), Times.Once);
    }

    [Test]
    public void CreateAsync_WithInvalidAccount_ShouldThrowValidationException()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            // Missing required fields
            Code = string.Empty,
            Name = string.Empty
        };

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GetPagedAsync_ShouldReturnPaginatedResults()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var accounts = new List<ChartOfAccount>
        {
            new ChartOfAccount { Code = "1.01.01", Name = "Account 1" },
            new ChartOfAccount { Code = "1.01.02", Name = "Account 2" }
        };
        int totalCount = 2;

        _repositoryMock.Setup(repo => repo.GetPagedAsync(page, pageSize, null, null))
            .ReturnsAsync((accounts, totalCount));

        // Act
        var result = await _service.GetPagedAsync(page, pageSize);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(totalCount);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        _repositoryMock.Verify(repo => repo.GetPagedAsync(page, pageSize, null, null), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldCallRepository()
    {
        // Arrange
        var code = "1.01.01";
        _repositoryMock.Setup(repo => repo.DeleteAsync(code))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(code);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteAsync(code), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WithPostableParentCode_ShouldThrowValidationException()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            Code = "1.01.02.01",
            Name = "Test Account",
            IsPostable = true,
            Type = "ATIVO",
            ParentCode = "1.01.02",
            CodeNormalized = "1.01.02.01",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        _repositoryMock.Setup(repo => repo.IsPostableAsync(account.ParentCode))
            .ReturnsAsync(true);

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        await action.Should().ThrowAsync<BusinessRuleValidationException>();
    }

    [Test]
    public async Task CreateAsync_WithDuplicateIdempotencyKey_ShouldThrowValidationException()
    {
        // Arrange
        var account = new ChartOfAccount
        {
            Code = "1.01.02",
            Name = "Test Account",
            IsPostable = true,
            Type = "ATIVO",
            CodeNormalized = "1.01.02",
            RequestId = "123",
            TenantId = Guid.NewGuid(),
            IdempotencyKey = Guid.NewGuid()
        };

        _repositoryMock.Setup(repo => repo.GetByIdempotencyKeyAsync(account.IdempotencyKey!.Value))
            .ReturnsAsync(new ChartOfAccount { Code = "1.01.03" }); 

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        await action.Should().ThrowAsync<BusinessRuleValidationException>();
    }
}