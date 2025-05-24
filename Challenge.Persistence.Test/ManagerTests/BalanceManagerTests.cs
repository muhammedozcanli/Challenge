using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Abstract;
using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Challenge.Persistence.Test.ManagerTests
{
    public class BalanceManagerTests
    {
        private readonly Mock<IBalanceRepository> _mockBalanceRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BalanceManager _balanceManager;

        public BalanceManagerTests()
        {
            _mockBalanceRepository = new Mock<IBalanceRepository>();
            _mockMapper = new Mock<IMapper>();
            _balanceManager = new BalanceManager(_mockBalanceRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetBalances_ShouldReturnBalanceDTOs_WhenBalancesExist()
        {
            // Arrange
            var balances = new List<Balance>
            {
                new Balance { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AvailableBalance = 1000, BlockedBalance = 0, Currency = "USD" },
                new Balance { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AvailableBalance = 2000, BlockedBalance = 0, Currency = "USD" }
            };

            var balanceDtos = new List<BalanceDTO>
            {
                new BalanceDTO { Id = balances[0].Id, UserId = balances[0].UserId, AvailableBalance = 1000, BlockedBalance = 0, Currency = "USD" },
                new BalanceDTO { Id = balances[1].Id, UserId = balances[1].UserId, AvailableBalance = 2000, BlockedBalance = 0, Currency = "USD" }
            };

            _mockBalanceRepository.Setup(repo => repo.GetList(It.IsAny<Expression<Func<Balance, bool>>>())).Returns(balances);
            _mockMapper.Setup(mapper => mapper.Map<List<BalanceDTO>>(balances)).Returns(balanceDtos);

            // Act
            var result = _balanceManager.GetBalances();

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(balanceDtos);
            _mockBalanceRepository.Verify(repo => repo.GetList(It.IsAny<Expression<Func<Balance, bool>>>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<BalanceDTO>>(balances), Times.Once);
        }

        [Fact]
        public void UpdateBalance_ShouldReturnTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var balanceId = Guid.NewGuid();
            var balanceDto = new BalanceDTO 
            { 
                Id = balanceId,
                UserId = Guid.NewGuid(),
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };

            var existingBalance = new Balance
            {
                Id = balanceId,
                UserId = balanceDto.UserId.GetValueOrDefault(),
                AvailableBalance = 500,
                BlockedBalance = 0,
                Currency = "USD"
            };

            _mockBalanceRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()))
                .Returns(existingBalance);

            // Act
            var result = _balanceManager.UpdateBalance(balanceDto);

            // Assert
            Assert.True(result);
            _mockBalanceRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()), Times.Once);
            _mockBalanceRepository.Verify(repo => repo.Update(existingBalance), Times.Once);
            _mockBalanceRepository.Verify(repo => repo.SaveChanges(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(balanceDto, existingBalance), Times.Once);
        }

        [Fact]
        public void UpdateBalance_ShouldReturnFalse_WhenBalanceDoesNotExist()
        {
            // Arrange
            var balanceDto = new BalanceDTO
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };

            _mockBalanceRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()))
                .Returns((Balance)null);

            // Act
            var result = _balanceManager.UpdateBalance(balanceDto);

            // Assert
            Assert.False(result);
            _mockBalanceRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()), Times.Once);
            _mockBalanceRepository.Verify(repo => repo.Update(It.IsAny<Balance>()), Times.Never);
            _mockBalanceRepository.Verify(repo => repo.SaveChanges(), Times.Never);
        }

        [Fact]
        public void GetBalanceByUserId_ShouldReturnBalanceDTO_WhenBalanceExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };

            var balanceDto = new BalanceDTO
            {
                Id = balance.Id,
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };

            _mockBalanceRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()))
                .Returns(balance);
            _mockMapper.Setup(mapper => mapper.Map<BalanceDTO>(balance))
                .Returns(balanceDto);

            // Act
            var result = _balanceManager.GetBalanceByUserId(userId);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(balanceDto);
            _mockBalanceRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<BalanceDTO>(balance), Times.Once);
        }

        [Fact]
        public void GetBalanceByUserId_ShouldReturnNull_WhenBalanceDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockBalanceRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()))
                .Returns((Balance)null);

            // Act
            var result = _balanceManager.GetBalanceByUserId(userId);

            // Assert
            Assert.Null(result);
            _mockBalanceRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Balance, bool>>>()), Times.Once);
        }
    }
} 