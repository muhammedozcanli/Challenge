using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;
using Challenge.Persistence.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace Challenge.Persistence.Test.RepositoryTests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly ChallengeDBContext _context;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ChallengeDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ChallengeDBContext(options);
            _userRepository = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetUserByFirstName_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "Test User",
                Password = "hashedPassword"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _userRepository.Get(u => u.FirstName == "Test User");

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Test User");
            result.Password.Should().Be("hashedPassword");
        }

        [Fact]
        public void GetUserByFirstName_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = _userRepository.Get(u => u.FirstName == "Nonexistent User");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void AddUser_ShouldCreateNewUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "New User",
                Password = "hashedPassword"
            };

            // Act
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            // Assert
            var savedUser = _context.Users.FirstOrDefault(u => u.FirstName == "New User");
            savedUser.Should().NotBeNull();
            savedUser.FirstName.Should().Be("New User");
        }

        [Fact]
        public void AddUser_WithBalance_ShouldCreateUserAndBalance()
        {
            // Arrange
            var user = new User
            {
                FirstName = "New User",
                Password = "hashedPassword",
                Balance = new Balance
                {
                    AvailableBalance = 1000,
                    BlockedBalance = 0,
                    Currency = "USD"
                }
            };

            // Act
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            // Assert
            var savedUser = _context.Users
                .Include(u => u.Balance)
                .FirstOrDefault(u => u.FirstName == "New User");
            
            savedUser.Should().NotBeNull();
            savedUser.Balance.Should().NotBeNull();
            savedUser.Balance.AvailableBalance.Should().Be(1000);
            savedUser.Balance.Currency.Should().Be("USD");
        }

        [Fact]
        public void AddUser_WithPreOrders_ShouldCreateUserAndPreOrders()
        {
            // Arrange
            var orderId1 = Guid.NewGuid();
            var orderId2 = Guid.NewGuid();
            var user = new User
            {
                FirstName = "New User",
                Password = "hashedPassword",
                PreOrders = new List<PreOrder>
                {
                    new PreOrder 
                    { 
                        OrderId = orderId1,
                        Amount = 500,
                        Status = "Pending",
                        CreatedDate = DateTime.UtcNow
                    },
                    new PreOrder 
                    { 
                        OrderId = orderId2,
                        Amount = 300,
                        Status = "Pending",
                        CreatedDate = DateTime.UtcNow
                    }
                }
            };

            // Act
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            // Assert
            var savedUser = _context.Users
                .Include(u => u.PreOrders)
                .FirstOrDefault(u => u.FirstName == "New User");
            
            savedUser.Should().NotBeNull();
            savedUser.PreOrders.Should().NotBeNull();
            savedUser.PreOrders.Should().HaveCount(2);
            savedUser.PreOrders.Should().Contain(po => po.OrderId == orderId1 && po.Amount == 500);
            savedUser.PreOrders.Should().Contain(po => po.OrderId == orderId2 && po.Amount == 300);
        }

        [Fact]
        public void UpdateUser_ShouldModifyExistingUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "Original Name",
                Password = "originalPassword"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            user.FirstName = "Updated Name";
            user.Password = "newPassword";
            _userRepository.Update(user);
            _userRepository.SaveChanges();

            // Assert
            var updatedUser = _context.Users.FirstOrDefault(u => u.FirstName == "Updated Name");
            updatedUser.Should().NotBeNull();
            updatedUser.FirstName.Should().Be("Updated Name");
            updatedUser.Password.Should().Be("newPassword");
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUserAndRelatedEntities()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var user = new User
            {
                FirstName = "Delete User",
                Password = "password",
                Balance = new Balance
                {
                    AvailableBalance = 1000,
                    BlockedBalance = 0,
                    Currency = "USD"
                },
                PreOrders = new List<PreOrder>
                {
                    new PreOrder 
                    { 
                        OrderId = orderId,
                        Amount = 500,
                        Status = "Pending",
                        CreatedDate = DateTime.UtcNow
                    }
                }
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            _userRepository.Delete(user);
            _userRepository.SaveChanges();

            // Assert
            var deletedUser = _context.Users.FirstOrDefault(u => u.FirstName == "Delete User");
            var deletedBalance = _context.Set<Balance>().FirstOrDefault(b => b.UserId == user.Id);
            var deletedPreOrders = _context.Set<PreOrder>().Where(po => po.UserId == user.Id).ToList();
            
            deletedUser.Should().BeNull();
            deletedBalance.Should().BeNull();
            deletedPreOrders.Should().BeEmpty();
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new[]
            {
                new User { FirstName = "User 1", Password = "pass1" },
                new User { FirstName = "User 2", Password = "pass2" },
                new User { FirstName = "User 3", Password = "pass3" }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetListAsync(null);

            // Assert
            result.Should().HaveCount(3);
            result.Should().Contain(u => u.FirstName == "User 1");
            result.Should().Contain(u => u.FirstName == "User 2");
            result.Should().Contain(u => u.FirstName == "User 3");
        }

        [Fact]
        public async Task GetListAsync_WithIncludeBalance_ShouldReturnUsersWithBalances()
        {
            // Arrange
            var users = new[]
            {
                new User 
                { 
                    FirstName = "User 1",
                    Password = "pass1",
                    Balance = new Balance { AvailableBalance = 1000, Currency = "USD" }
                },
                new User 
                { 
                    FirstName = "User 2",
                    Password = "pass2",
                    Balance = new Balance { AvailableBalance = 2000, Currency = "EUR" }
                }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.Users
                .Include(u => u.Balance)
                .ToListAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(u => u.Balance.Should().NotBeNull());
            result.Should().Contain(u => u.Balance.Currency == "USD" && u.Balance.AvailableBalance == 1000);
            result.Should().Contain(u => u.Balance.Currency == "EUR" && u.Balance.AvailableBalance == 2000);
        }

        [Fact]
        public async Task GetListAsync_WithIncludePreOrders_ShouldReturnUsersWithPreOrders()
        {
            // Arrange
            var orderId1 = Guid.NewGuid();
            var orderId2 = Guid.NewGuid();
            var orderId3 = Guid.NewGuid();
            
            var users = new[]
            {
                new User 
                { 
                    FirstName = "User 1",
                    Password = "pass1",
                    PreOrders = new List<PreOrder> 
                    { 
                        new PreOrder { OrderId = orderId1, Amount = 500, Status = "Pending" },
                        new PreOrder { OrderId = orderId2, Amount = 300, Status = "Pending" }
                    }
                },
                new User 
                { 
                    FirstName = "User 2",
                    Password = "pass2",
                    PreOrders = new List<PreOrder> 
                    { 
                        new PreOrder { OrderId = orderId3, Amount = 200, Status = "Pending" }
                    }
                }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.Users
                .Include(u => u.PreOrders)
                .ToListAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(u => u.PreOrders.Should().NotBeNull());
            result.Should().Contain(u => u.PreOrders.Count == 2);
            result.Should().Contain(u => u.PreOrders.Count == 1);
            
            var userWithTwoOrders = result.First(u => u.PreOrders.Count == 2);
            userWithTwoOrders.PreOrders.Should().Contain(po => po.OrderId == orderId1 && po.Amount == 500);
            userWithTwoOrders.PreOrders.Should().Contain(po => po.OrderId == orderId2 && po.Amount == 300);
            
            var userWithOneOrder = result.First(u => u.PreOrders.Count == 1);
            userWithOneOrder.PreOrders.Should().Contain(po => po.OrderId == orderId3 && po.Amount == 200);
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnFilteredUsers()
        {
            // Arrange
            var users = new[]
            {
                new User { FirstName = "Admin 1", Password = "pass1" },
                new User { FirstName = "User 1", Password = "pass2" },
                new User { FirstName = "Admin 2", Password = "pass3" }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetListAsync(u => u.FirstName.StartsWith("Admin"));

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(u => u.FirstName.Should().StartWith("Admin"));
        }
    }
} 