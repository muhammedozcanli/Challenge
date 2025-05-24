using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;
using Challenge.Persistence.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace Challenge.Persistence.Test.RepositoryTests
{
    public class BalanceRepositoryTests : IDisposable
    {
        private readonly ChallengeDBContext _context;
        private readonly IBalanceRepository _balanceRepository;

        public BalanceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ChallengeDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ChallengeDBContext(options);
            _balanceRepository = new BalanceRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetBalanceByUserId_ShouldReturnCorrectBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };
            _context.Balances.Add(balance);
            _context.SaveChanges();

            // Act
            var result = _balanceRepository.Get(b => b.UserId == userId);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.AvailableBalance.Should().Be(1000);
            result.BlockedBalance.Should().Be(0);
            result.Currency.Should().Be("USD");
        }

        [Fact]
        public void AddBalance_ShouldCreateNewBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                UserId = userId,
                AvailableBalance = 500,
                BlockedBalance = 100,
                Currency = "EUR"
            };

            // Act
            _balanceRepository.Add(balance);
            _balanceRepository.SaveChanges();

            // Assert
            var savedBalance = _context.Balances.FirstOrDefault(b => b.UserId == userId);
            savedBalance.Should().NotBeNull();
            savedBalance.AvailableBalance.Should().Be(500);
            savedBalance.BlockedBalance.Should().Be(100);
            savedBalance.Currency.Should().Be("EUR");
        }

        [Fact]
        public void UpdateBalance_ShouldModifyExistingBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };
            _context.Balances.Add(balance);
            _context.SaveChanges();

            // Act
            balance.AvailableBalance = 800;
            balance.BlockedBalance = 200;
            _balanceRepository.Update(balance);
            _balanceRepository.SaveChanges();

            // Assert
            var updatedBalance = _context.Balances.FirstOrDefault(b => b.UserId == userId);
            updatedBalance.Should().NotBeNull();
            updatedBalance.AvailableBalance.Should().Be(800);
            updatedBalance.BlockedBalance.Should().Be(200);
        }

        [Fact]
        public void DeleteBalance_ShouldRemoveBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };
            _context.Balances.Add(balance);
            _context.SaveChanges();

            // Act
            _balanceRepository.Delete(balance);
            _balanceRepository.SaveChanges();

            // Assert
            var deletedBalance = _context.Balances.FirstOrDefault(b => b.UserId == userId);
            deletedBalance.Should().BeNull();
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnAllBalances()
        {
            // Arrange
            var balances = new[]
            {
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 1000, BlockedBalance = 0, Currency = "USD" },
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 2000, BlockedBalance = 500, Currency = "EUR" },
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 3000, BlockedBalance = 1000, Currency = "GBP" }
            };
            await _context.Balances.AddRangeAsync(balances);
            await _context.SaveChangesAsync();

            // Act
            var result = await _balanceRepository.GetListAsync(null);

            // Assert
            result.Should().HaveCount(3);
            result.Should().Contain(b => b.Currency == "USD" && b.AvailableBalance == 1000);
            result.Should().Contain(b => b.Currency == "EUR" && b.AvailableBalance == 2000);
            result.Should().Contain(b => b.Currency == "GBP" && b.AvailableBalance == 3000);
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnFilteredBalances()
        {
            // Arrange
            var balances = new[]
            {
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 1000, BlockedBalance = 0, Currency = "USD" },
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 2000, BlockedBalance = 500, Currency = "USD" },
                new Balance { UserId = Guid.NewGuid(), AvailableBalance = 3000, BlockedBalance = 1000, Currency = "EUR" }
            };
            await _context.Balances.AddRangeAsync(balances);
            await _context.SaveChangesAsync();

            // Act
            var result = await _balanceRepository.GetListAsync(b => b.Currency == "USD");

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(b => b.Currency.Should().Be("USD"));
        }

        [Fact]
        public void GetBalance_ShouldHandleNonExistentBalance()
        {
            // Act
            var result = _balanceRepository.Get(b => b.UserId == Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateBalance_ShouldHandleConcurrentUpdates()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = new Balance
            {
                UserId = userId,
                AvailableBalance = 1000,
                BlockedBalance = 0,
                Currency = "USD"
            };
            _context.Balances.Add(balance);
            _context.SaveChanges();

            // Act & Assert
            var balance1 = _balanceRepository.Get(b => b.UserId == userId);
            var balance2 = _balanceRepository.Get(b => b.UserId == userId);

            balance1.AvailableBalance = 800;
            balance2.AvailableBalance = 900;

            _balanceRepository.Update(balance1);
            _balanceRepository.SaveChanges();

            _balanceRepository.Update(balance2);
            _balanceRepository.SaveChanges();

            var finalBalance = _balanceRepository.Get(b => b.UserId == userId);
            finalBalance.AvailableBalance.Should().Be(900);
        }
    }
} 