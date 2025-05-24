using Moq;
using Challenge.Business.ErrorOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Xunit;

namespace Challenge.Business.Test.ErrorTests
{
    public class ErrorOperationsTests
    {
        private readonly Mock<IErrorManager> _mockErrorManager;
        private readonly IErrorOperations _errorOperations;

        public ErrorOperationsTests()
        {
            _mockErrorManager = new Mock<IErrorManager>();
            _errorOperations = new ErrorOperations.ErrorOperations(_mockErrorManager.Object);
        }

        [Fact]
        public void AddError_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var errorDto = new ErrorDTO
            {
                Name = "Test Error",
                Message = "Test Message"
            };
            _mockErrorManager.Setup(x => x.AddError(It.IsAny<ErrorDTO>())).Returns(true);

            // Act
            var result = _errorOperations.AddError(errorDto);

            // Assert
            Assert.True(result);
            _mockErrorManager.Verify(x => x.AddError(errorDto), Times.Once);
        }

        [Fact]
        public void AddError_ShouldReturnFalse_WhenFailed()
        {
            // Arrange
            var errorDto = new ErrorDTO
            {
                Name = "Test Error",
                Message = "Test Message"
            };
            _mockErrorManager.Setup(x => x.AddError(It.IsAny<ErrorDTO>())).Returns(false);

            // Act
            var result = _errorOperations.AddError(errorDto);

            // Assert
            Assert.False(result);
            _mockErrorManager.Verify(x => x.AddError(errorDto), Times.Once);
        }
    }
} 