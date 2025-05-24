using Challenge.Business.UserOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Moq;
using Xunit;
using FluentAssertions;

namespace Challenge.Business.Test.UserTests
{
    public class UserOperationsTests
    {
        private readonly Mock<IUserManager> _mockUserManager;
        private readonly IUserOperations _userOperations;

        public UserOperationsTests()
        {
            _mockUserManager = new Mock<IUserManager>();
            _userOperations = new UserOperations.UserOperations(_mockUserManager.Object);
        }

        [Fact]
        public void GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new UserDTO 
            { 
                Id = userId, 
                FirstName = "Test User"
            };
            _mockUserManager.Setup(manager => manager.GetUser(userId)).Returns(userDto);

            // Act
            var result = _userOperations.GetUser(userId);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public void GetUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserManager.Setup(manager => manager.GetUser(userId)).Returns((UserDTO)null);

            // Act
            var result = _userOperations.GetUser(userId);

            // Assert
            Assert.Null(result);
        }
    }
} 