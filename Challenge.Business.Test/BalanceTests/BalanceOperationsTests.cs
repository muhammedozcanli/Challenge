using Moq;
using Challenge.Business.BalanceOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Challenge.Business.Test.BalanceTests
{
    public class BalanceOperationsTests
    {
        private readonly Mock<IBalanceManager> _mockBalanceManager;
        private readonly IBalanceOperations _balanceOperations;

        public BalanceOperationsTests()
        {
            _mockBalanceManager = new Mock<IBalanceManager>();
            _balanceOperations = new BalanceOperations.BalanceOperations(_mockBalanceManager.Object);
        }

        [Fact]
        public void GetBalances_ShouldReturnBalanceList_WhenBalancesExist()
        {
            // Arrange
            var balances = new List<BalanceDTO>
            {
                new BalanceDTO { UserId = Guid.NewGuid(), AvailableBalance = 1000, BlockedBalance = 0, Currency = "USD" },
                new BalanceDTO { UserId = Guid.NewGuid(), AvailableBalance = 3000, BlockedBalance = 0, Currency = "USD" }
            };

            _mockBalanceManager.Setup(manager => manager.GetBalances()).Returns(balances);

            // Act
            var result = _balanceOperations.GetBalances();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            result.Should().BeEquivalentTo(balances);
        }

        [Fact]
        public void GetBalances_ShouldReturnEmptyList_WhenNoBalancesExist()
        {
            // Arrange
            _mockBalanceManager.Setup(manager => manager.GetBalances()).Returns(new List<BalanceDTO>());

            // Act
            var result = _balanceOperations.GetBalances();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void UpdateBalance_ShouldReturnTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var balanceDto = new BalanceDTO
            {
                UserId = Guid.NewGuid(),
                AvailableBalance = 1000,
                BlockedBalance = 200,
                Currency = "USD"
            };

            _mockBalanceManager.Setup(manager => manager.UpdateBalance(It.IsAny<BalanceDTO>())).Returns(true);

            // Act
            var result = _balanceOperations.UpdateBalance(balanceDto);

            // Assert
            Assert.True(result);
            _mockBalanceManager.Verify(manager => manager.UpdateBalance(It.Is<BalanceDTO>(b => 
                b.UserId == balanceDto.UserId && 
                b.AvailableBalance == balanceDto.AvailableBalance && 
                b.BlockedBalance == balanceDto.BlockedBalance && 
                b.Currency == balanceDto.Currency)), Times.Once);
        }

        [Fact]
        public void UpdateBalance_ShouldReturnFalse_WhenUpdateFails()
        {
            // Arrange
            var balanceDto = new BalanceDTO
            {
                UserId = Guid.NewGuid(),
                AvailableBalance = 1000,
                BlockedBalance = 200,
                Currency = "USD"
            };

            _mockBalanceManager.Setup(manager => manager.UpdateBalance(It.IsAny<BalanceDTO>())).Returns(false);

            // Act
            var result = _balanceOperations.UpdateBalance(balanceDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetBalanceByUserId_ShouldReturnBalance_WhenBalanceExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balanceDto = new BalanceDTO 
            { 
                UserId = userId, 
                AvailableBalance = 1000, 
                BlockedBalance = 0,
                Currency = "USD"
            };
            _mockBalanceManager.Setup(manager => manager.GetBalanceByUserId(userId)).Returns(balanceDto);

            // Act
            var result = _balanceOperations.GetBalanceByUserId(userId);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(balanceDto);
        }

        [Fact]
        public void GetBalanceByUserId_ShouldReturnNull_WhenBalanceDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockBalanceManager.Setup(manager => manager.GetBalanceByUserId(userId)).Returns((BalanceDTO)null);

            // Act
            var result = _balanceOperations.GetBalanceByUserId(userId);

            // Assert
            Assert.Null(result);
        }
    }
} 