using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Abstract;
using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq.Expressions;

namespace Challenge.Persistence.Test.ManagerTests
{
    public class UserManagerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserManager _userManager;

        public UserManagerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userManager = new UserManager(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetUser_ShouldReturnUserDTO_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FirstName = "Test User" };
            var userDto = new UserDTO { Id = userId, FirstName = "Test User" };

            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(user);
            _mockMapper.Setup(mapper => mapper.Map<UserDTO>(user))
                .Returns(userDto);

            // Act
            var result = _userManager.GetUser(userId);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(userDto);
            _mockUserRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<UserDTO>(user), Times.Once);
        }

        [Fact]
        public void GetUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((User)null);

            // Act
            var result = _userManager.GetUser(userId);

            // Assert
            Assert.Null(result);
            _mockUserRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
} 