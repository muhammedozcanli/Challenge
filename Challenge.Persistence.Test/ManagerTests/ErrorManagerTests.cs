using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Abstract;
using Moq;
using Xunit;

namespace Challenge.Persistence.Test.ManagerTests
{
    public class ErrorManagerTests
    {
        private readonly Mock<IErrorRepository> _mockErrorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ErrorManager _errorManager;

        public ErrorManagerTests()
        {
            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockMapper = new Mock<IMapper>();
            _errorManager = new ErrorManager(_mockErrorRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddError_ShouldReturnTrue_WhenErrorIsAddedSuccessfully()
        {
            // Arrange
            var errorDto = new ErrorDTO
            {
                Name = "Test Error",
                Message = "Test Message"
            };

            var error = new Error
            {
                Name = "Test Error",
                Message = "Test Message"
            };

            _mockMapper.Setup(mapper => mapper.Map<Error>(errorDto)).Returns(error);

            // Act
            var result = _errorManager.AddError(errorDto);

            // Assert
            Assert.True(result);
            _mockMapper.Verify(mapper => mapper.Map<Error>(errorDto), Times.Once);
            _mockErrorRepository.Verify(repo => repo.Add(error), Times.Once);
            _mockErrorRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddError_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var errorDto = new ErrorDTO
            {
                Name = "Test Error",
                Message = "Test Message"
            };

            _mockMapper.Setup(mapper => mapper.Map<Error>(errorDto))
                .Throws(new Exception("Test exception"));

            // Act
            var result = _errorManager.AddError(errorDto);

            // Assert
            Assert.False(result);
            _mockMapper.Verify(mapper => mapper.Map<Error>(errorDto), Times.Once);
            _mockErrorRepository.Verify(repo => repo.Add(It.IsAny<Error>()), Times.Never);
            _mockErrorRepository.Verify(repo => repo.SaveChanges(), Times.Never);
        }
    }
} 