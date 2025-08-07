using ChartOfAccounts.Api.Controllers.v1;
using ChartOfAccounts.Application.DTOs.ChartOfAccounts;
using ChartOfAccounts.Application.DTOs.Common;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ChartOfAccounts.Tests.Api.Controllers;

[TestFixture]
public class AccountsControllerTests
{
    private Mock<IChartOfAccountsService> _serviceMock;
    private Mock<IRequestContext> _requestContextMock;
    private Mock<IChartOfAccountFactory> _chartOfAccountFactoryMock;
    private AccountsController _controller;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IChartOfAccountsService>();
        _requestContextMock = new Mock<IRequestContext>();
        _chartOfAccountFactoryMock = new Mock<IChartOfAccountFactory>();
        
        _controller = new AccountsController(
            _serviceMock.Object,
            _requestContextMock.Object,
            _chartOfAccountFactoryMock.Object);
    }

    [Test]
    public async Task GetByCode_WhenCodeExists_ShouldReturnOk()
    {
        // Arrange
        var code = "1.01.01";
        var account = new ChartOfAccount
        {
            Code = code,
            Name = "Test Account",
            IsPostable = true
        };

        _serviceMock.Setup(s => s.GetByCodeAsync(code))
            .ReturnsAsync(account);

        // Act
        var result = await _controller.GetByCode(code);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult?.Value.Should().BeOfType<ChartOfAccountResponseDto>();
        var dto = okResult?.Value as ChartOfAccountResponseDto;
        dto?.Code.Should().Be(code);
    }

    [Test]
    public async Task GetByCode_WhenCodeDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var code = "nonexistent";
        _serviceMock.Setup(s => s.GetByCodeAsync(code))
            .ReturnsAsync((ChartOfAccount)null);

        // Act
        var result = await _controller.GetByCode(code);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }
}