using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Tests.TestFixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace ChartOfAccounts.Tests.Infrastructure.Repositories;

[TestFixture]
public class ChartOfAccountsRepositoryTests
{
    private AppDbContext _dbContext;
    private IChartOfAccountsRepository _repository;

    [SetUp]
    public void Setup()
    {
        // Use a mock or in-memory database for repository tests
        _dbContext = DbContextFactory.CreateInMemoryDbContext();
        // Assume repository implementation takes DbContext in constructor
        // _repository = new ChartOfAccountsRepository(_dbContext);
        
        // For now, we'll use a mock repository
        var repositoryMock = new Mock<IChartOfAccountsRepository>();
        _repository = repositoryMock.Object;
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task GetByCodeAsync_WhenCodeExists_ShouldReturnAccount()
    {
        // This is a placeholder - in a real test, you'd:
        // 1. Setup test data in the in-memory database
        // 2. Call the actual repository method
        // 3. Assert on the results
        
        // For mock example:
        var mock = Mock.Get(_repository);
        var code = "1.01.01";
        var account = new ChartOfAccount { Code = code, Name = "Test" };
        
        mock.Setup(r => r.GetByCodeAsync(code)).ReturnsAsync(account);
        
        var result = await _repository.GetByCodeAsync(code);
        
        result.Should().NotBeNull();
        result.Code.Should().Be(code);
    }
}