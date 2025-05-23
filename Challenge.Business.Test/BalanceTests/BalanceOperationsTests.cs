using Moq;
using Challenge.Business.BalanceOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using FluentAssertions;
using System.Collections.Generic;

namespace Challenge.Business.Test.BalanceTests;

public class BalanceOperationsTests
{
    private readonly Mock<IBalanceManager> _balanceManagerMock;
    private readonly IBalanceOperations _balanceOperations;

    public BalanceOperationsTests()
    {
        // Setup
        _balanceManagerMock = new Mock<IBalanceManager>();
        _balanceOperations = new BalanceOperations.BalanceOperations(_balanceManagerMock.Object);
    }

    [Fact]
    public void GetBalances_WhenCalled_ShouldReturnBalanceList()
    {
        // Arrange
        var expectedBalances = new List<BalanceDTO>
        {
            new BalanceDTO { UserId = Guid.NewGuid(), AvailableBalance= 1000, BlockedBalance = 0, Currency = "USD",  },
            new BalanceDTO { UserId = Guid.NewGuid(), AvailableBalance= 3000, BlockedBalance = 0, Currency = "USD", }
        };

        _balanceManagerMock
            .Setup(bm => bm.GetBalances())
            .Returns(expectedBalances);

        // Act
        var actualBalances = _balanceOperations.GetBalances();

        // Assert
        actualBalances.Should().BeEquivalentTo(expectedBalances);
        _balanceManagerMock.Verify(bm => bm.GetBalances(), Times.Once);
    }

    [Fact]
    public void GetBalances_WhenNoBalances_ShouldReturnEmptyList()
    {
        // Arrange
        var emptyList = new List<BalanceDTO>();
        _balanceManagerMock
            .Setup(bm => bm.GetBalances())
            .Returns(emptyList);

        // Act
        var result = _balanceOperations.GetBalances();

        // Assert
        result.Should().BeEmpty();
        _balanceManagerMock.Verify(bm => bm.GetBalances(), Times.Once);
    }

    [Fact]
    public void UpdateBalance_WhenValidBalance_ShouldReturnTrue()
    {
        // Arrange
        var balanceDTO = new BalanceDTO 
        { 
            UserId = Guid.NewGuid(),
            AvailableBalance = 3000,
            BlockedBalance = 0,
            Currency = "USD",
        };

        _balanceManagerMock
            .Setup(bm => bm.UpdateBalance(balanceDTO))
            .Returns(true);

        // Act
        var result = _balanceOperations.UpdateBalance(balanceDTO);

        // Assert
        result.Should().BeTrue();
        _balanceManagerMock.Verify(bm => bm.UpdateBalance(balanceDTO), Times.Once);
    }

    [Fact]
    public void UpdateBalance_WhenInvalidBalance_ShouldReturnFalse()
    {
        // Arrange
        var balanceDTO = new BalanceDTO 
        { 
            UserId = Guid.NewGuid(),
            AvailableBalance = 3000,
            BlockedBalance = 0,
            Currency = "USD",
        };

        _balanceManagerMock
            .Setup(bm => bm.UpdateBalance(balanceDTO))
            .Returns(false);

        // Act
        var result = _balanceOperations.UpdateBalance(balanceDTO);

        // Assert
        result.Should().BeFalse();
        _balanceManagerMock.Verify(bm => bm.UpdateBalance(balanceDTO), Times.Once);
    }

    [Fact]
    public void GetBalanceByUserId_WhenValidUserId_ShouldReturnBalance()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedBalance = new BalanceDTO 
        { 
            UserId = userId,
            AvailableBalance = 3000,
            BlockedBalance = 0,
            Currency = "USD",
        };

        _balanceManagerMock
            .Setup(bm => bm.GetBalanceByUserId(userId))
            .Returns(expectedBalance);

        // Act
        var result = _balanceOperations.GetBalanceByUserId(userId);

        // Assert
        result.Should().BeEquivalentTo(expectedBalance);
        _balanceManagerMock.Verify(bm => bm.GetBalanceByUserId(userId), Times.Once);
    }

    [Fact]
    public void GetBalanceByUserId_WhenUserNotFound_ShouldReturnNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _balanceManagerMock
            .Setup(bm => bm.GetBalanceByUserId(userId))
            .Returns((BalanceDTO)null);

        // Act
        var result = _balanceOperations.GetBalanceByUserId(userId);

        // Assert
        result.Should().BeNull();
        _balanceManagerMock.Verify(bm => bm.GetBalanceByUserId(userId), Times.Once);
    }

    [Fact]
    public void GetBalanceByUserId_WhenManagerThrowsException_ShouldPropagateException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _balanceManagerMock
            .Setup(bm => bm.GetBalanceByUserId(userId))
            .Throws<Exception>();

        // Act & Assert
        Assert.Throws<Exception>(() => _balanceOperations.GetBalanceByUserId(userId));
        _balanceManagerMock.Verify(bm => bm.GetBalanceByUserId(userId), Times.Once);
    }
} 