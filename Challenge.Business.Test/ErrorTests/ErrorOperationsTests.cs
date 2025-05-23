using Moq;
using Challenge.Business.ErrorOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using FluentAssertions;

namespace Challenge.Business.Test.ErrorTests;

public class ErrorOperationsTests
{
    private readonly Mock<IErrorManager> _errorManagerMock;
    private readonly IErrorOperations _errorOperations;

    public ErrorOperationsTests()
    {
        // Setup
        _errorManagerMock = new Mock<IErrorManager>();
        _errorOperations = new ErrorOperations.ErrorOperations(_errorManagerMock.Object);
    }

    [Fact]
    public void AddError_WhenValidError_ShouldReturnTrue()
    {
        // Arrange
        var errorDTO = new ErrorDTO 
        { 
            Id = Guid.NewGuid(),
            Name = "TEST001",
            Message = "Test Error Message",
        };

        _errorManagerMock
            .Setup(em => em.AddError(errorDTO))
            .Returns(true);

        // Act
        var result = _errorOperations.AddError(errorDTO);

        // Assert
        result.Should().BeTrue();
        _errorManagerMock.Verify(em => em.AddError(errorDTO), Times.Once);
    }

    [Fact]
    public void AddError_WhenInvalidError_ShouldReturnFalse()
    {
        // Arrange
        var errorDTO = new ErrorDTO 
        { 
            Id = Guid.NewGuid(),
            Name = "TEST001",
            Message = "Test Error Message",
        };

        _errorManagerMock
            .Setup(em => em.AddError(errorDTO))
            .Returns(false);

        // Act
        var result = _errorOperations.AddError(errorDTO);

        // Assert
        result.Should().BeFalse();
        _errorManagerMock.Verify(em => em.AddError(errorDTO), Times.Once);
    }

    [Fact]
    public void AddError_WhenNullError_ShouldReturnFalse()
    {
        // Arrange
        ErrorDTO errorDTO = null;

        _errorManagerMock
            .Setup(em => em.AddError(null))
            .Returns(false);

        // Act
        var result = _errorOperations.AddError(errorDTO);

        // Assert
        result.Should().BeFalse();
        _errorManagerMock.Verify(em => em.AddError(null), Times.Once);
    }

    [Fact]
    public void AddError_WhenManagerThrowsException_ShouldPropagateException()
    {
        // Arrange
        var errorDTO = new ErrorDTO 
        { 
            Id = Guid.NewGuid(),
            Name = "TEST001",
            Message = "Test Error Message",
        };

        _errorManagerMock
            .Setup(em => em.AddError(errorDTO))
            .Throws<Exception>();

        // Act & Assert
        Assert.Throws<Exception>(() => _errorOperations.AddError(errorDTO));
        _errorManagerMock.Verify(em => em.AddError(errorDTO), Times.Once);
    }
} 