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
    private readonly Mock<IProductManager> _mockProductManager;
    private readonly IProductOperations _productOperations;

    public ProductOperationsTests()
    {
        _mockProductManager = new Mock<IProductManager>();
        _productOperations = new ProductOperations.ProductOperations(_mockProductManager.Object);
    }

    [Fact]
    public void GetProducts_ShouldReturnProducts_WhenProductsExist()
    {
        // Arrange
        var products = new List<ProductDTO>
        {
            new ProductDTO { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 10 },
            new ProductDTO { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 20 }
        };
        _mockProductManager.Setup(manager => manager.GetProducts()).Returns(products);

        // Act
        var result = _productOperations.GetProducts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        result.Should().BeEquivalentTo(products);
    }

    [Fact]
    public void GetProducts_ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // Arrange
        _mockProductManager.Setup(manager => manager.GetProducts()).Returns(new List<ProductDTO>());

        // Act
        var result = _productOperations.GetProducts();

        // Assert
        Assert.Empty(result);
    }
} 