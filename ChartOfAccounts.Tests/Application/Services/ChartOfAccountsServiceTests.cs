using ChartOfAccounts.Application.Services;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Domain.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions.Interfaces;
using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Domain.Enums;

namespace ChartOfAccounts.Tests.Application.Services;

[TestFixture]
public class ChartOfAccountsServiceTests
{
    private Mock<IChartOfAccountsRepository> _repositoryMock;
    private Mock<IDeleteValidationChainBuilder> _deleteValidationChainBuilderMock;
    private Mock<ICreateValidationChainBuilder> _createValidationChainBuilderMock;
    private Mock<IExceptionHandlerChainBuilder> _exceptionHandlerChainBuilderMock;
    private ChartOfAccountsService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IChartOfAccountsRepository>();
        _deleteValidationChainBuilderMock = new Mock<IDeleteValidationChainBuilder>();
        _createValidationChainBuilderMock = new Mock<ICreateValidationChainBuilder>();
        _exceptionHandlerChainBuilderMock = new Mock<IExceptionHandlerChainBuilder>();

        _service = new ChartOfAccountsService(
            _repositoryMock.Object,
            _deleteValidationChainBuilderMock.Object,
            _createValidationChainBuilderMock.Object,
            _exceptionHandlerChainBuilderMock.Object);
    }

    [Test]
    public async Task GetByCodeAsync_WhenCodeExists_ShouldReturnAccount()
    {
        // Arrange
        String code = "1.01.01";
        ChartOfAccount expectedAccount = new ChartOfAccount
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
        ChartOfAccount result = await _service.GetByCodeAsync(code);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAccount);
        _repositoryMock.Verify(repo => repo.GetByCodeAsync(code), Times.Once);
    }

    [Test]
    public async Task GetByCodeAsync_WhenCodeDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        String nonExistentCode = "9.99.99";
        _repositoryMock.Setup(repo => repo.GetByCodeAsync(nonExistentCode))
            .ReturnsAsync((ChartOfAccount)null);

        // Act
        ChartOfAccount result = await _service.GetByCodeAsync(nonExistentCode);

        // Assert
        result.Should().BeNull();
        _repositoryMock.Verify(repo => repo.GetByCodeAsync(nonExistentCode), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WithValidAccount_ShouldCallRepository()
    {
        // Arrange
        ChartOfAccount account = new ChartOfAccount
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

        _createValidationChainBuilderMock.Setup(builder => 
            builder.ValidateAsync(account)).Returns(Task.CompletedTask);
        
        _repositoryMock.Setup(repo => repo.CreateAsync(account))
            .Returns(Task.CompletedTask);

        // Act
        await _service.CreateAsync(account);

        // Assert
        _createValidationChainBuilderMock.Verify(builder => 
            builder.ValidateAsync(account), Times.Once);
        _repositoryMock.Verify(repo => repo.CreateAsync(account), Times.Once);
    }

    [Test]
    public void CreateAsync_WithInvalidAccount_ShouldThrowValidationException()
    {
        // Arrange
        ChartOfAccount account = new ChartOfAccount
        {
            // Missing required fields
            Code = String.Empty,
            Name = String.Empty
        };

        _createValidationChainBuilderMock.Setup(builder => 
            builder.ValidateAsync(account))
            .ThrowsAsync(new ValidationException("Validation failed"));

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GetPagedAsync_ShouldReturnPaginatedResults()
    {
        // Arrange
        Int32 page = 1;
        Int32 pageSize = 10;
        List<ChartOfAccount> accounts = new List<ChartOfAccount>
        {
            new ChartOfAccount { Code = "1.01.01", Name = "Account 1" },
            new ChartOfAccount { Code = "1.01.02", Name = "Account 2" }
        };
        Int32 totalCount = 2;

        _repositoryMock.Setup(repo => repo.GetPagedAsync(page, pageSize, null, null))
            .ReturnsAsync((accounts, totalCount));

        // Act
        PaginatedResultDto<ChartOfAccount> result = await _service.GetPagedAsync(page, pageSize);

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
        String code = "1.01.01";
        
        _deleteValidationChainBuilderMock.Setup(builder => 
            builder.ValidateAsync(code)).Returns(Task.CompletedTask);
            
        _repositoryMock.Setup(repo => repo.DeleteAsync(code))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(code);

        // Assert
        _deleteValidationChainBuilderMock.Verify(builder => 
            builder.ValidateAsync(code), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteAsync(code), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WithValidationFailure_ShouldThrowBusinessRuleValidationException()
    {
        // Arrange
        ChartOfAccount account = new ChartOfAccount
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

        _createValidationChainBuilderMock.Setup(builder => 
            builder.ValidateAsync(account))
            .ThrowsAsync(new BusinessRuleValidationException("Validation failed"));

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        await action.Should().ThrowAsync<BusinessRuleValidationException>();
    }

    [Test]
    public async Task CreateAsync_WithDataIntegrityViolation_ShouldUseExceptionHandler()
    {
        // Arrange
        ChartOfAccount account = new ChartOfAccount
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

        DataIntegrityViolationException exception = new DataIntegrityViolationException(
            "Duplicate entry");

        _createValidationChainBuilderMock.Setup(builder => 
            builder.ValidateAsync(account)).Returns(Task.CompletedTask);
        
        _repositoryMock.Setup(repo => repo.CreateAsync(account))
            .ThrowsAsync(exception);

        _exceptionHandlerChainBuilderMock.Setup(builder => 
            builder.HandleExceptionAsync(account, exception))
            .ThrowsAsync(new BusinessRuleValidationException("Code already exists"));

        // Act & Assert
        Func<Task> action = async () => await _service.CreateAsync(account);
        await action.Should().ThrowAsync<BusinessRuleValidationException>()
            .WithMessage("Code already exists");
    }
}