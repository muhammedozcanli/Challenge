using Xunit;
using Moq;
using Challenge.Business.ProductOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using FluentAssertions;
using System.Collections.Generic;

namespace Challenge.Business.Test.ProductTests;

public class ProductOperationsTests
{
    private readonly Mock<IProductManager> _productManagerMock;
    private readonly IProductOperations _productOperations;

    public ProductOperationsTests()
    {
        // Setup
        _productManagerMock = new Mock<IProductManager>();
        _productOperations = new ProductOperations.ProductOperations(_productManagerMock.Object);
    }

    [Fact]
    public void GetProducts_WhenCalled_ShouldReturnProductList()
    {
        // Arrange
        var expectedProducts = new List<ProductDTO>
        {
            new ProductDTO { Id = Guid.NewGuid(), Name = "Test Product 1", Price = 100.00 },
            new ProductDTO { Id = Guid.NewGuid(), Name = "Test Product 2", Price = 200.00 }
        };

        _productManagerMock
            .Setup(pm => pm.GetProducts())
            .Returns(expectedProducts);

        // Act
        var actualProducts = _productOperations.GetProducts();

        // Assert
        actualProducts.Should().BeEquivalentTo(expectedProducts);
        _productManagerMock.Verify(pm => pm.GetProducts(), Times.Once);
    }

    [Fact]
    public void GetProducts_WhenNoProducts_ShouldReturnEmptyList()
    {
        // Arrange
        var emptyList = new List<ProductDTO>();
        _productManagerMock
            .Setup(pm => pm.GetProducts())
            .Returns(emptyList);

        // Act
        var result = _productOperations.GetProducts();

        // Assert
        result.Should().BeEmpty();
        _productManagerMock.Verify(pm => pm.GetProducts(), Times.Once);
    }

    [Fact]
    public void GetProducts_WhenManagerThrowsException_ShouldPropagateException()
    {
        // Arrange
        _productManagerMock
            .Setup(pm => pm.GetProducts())
            .Throws<System.Exception>();

        // Act & Assert
        Assert.Throws<System.Exception>(() => _productOperations.GetProducts());
        _productManagerMock.Verify(pm => pm.GetProducts(), Times.Once);
    }
} 