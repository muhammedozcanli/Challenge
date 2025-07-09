using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Abstract;
using Moq;
using Xunit;
using FluentAssertions;

namespace Challenge.Persistence.Test.ManagerTests
{
    public class ProductManagerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductManager _productManager;

        public ProductManagerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _productManager = new ProductManager(_mockProductRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnProductDTOs_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 20 }
            };

            var productDtos = new List<ProductDTO>
            {
                new ProductDTO { Id = products[0].Id, Name = "Product 1", Price = 100, Stock = 10 },
                new ProductDTO { Id = products[1].Id, Name = "Product 2", Price = 200, Stock = 20 }
            };

            _mockProductRepository.Setup(repo => repo.GetListAsync(null)).ReturnsAsync(products);
            _mockMapper.Setup(mapper => mapper.Map<List<ProductDTO>>(products)).Returns(productDtos);

            // Act
            var result = await _productManager.GetProducts();

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(productDtos);
            _mockProductRepository.Verify(repo => repo.GetListAsync(null), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ProductDTO>>(products), Times.Once);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var products = new List<Product>();
            var productDtos = new List<ProductDTO>();

            _mockProductRepository.Setup(repo => repo.GetListAsync(null)).ReturnsAsync(products);
            _mockMapper.Setup(mapper => mapper.Map<List<ProductDTO>>(products)).Returns(productDtos);

            // Act
            var result = await _productManager.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockProductRepository.Verify(repo => repo.GetListAsync(null), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ProductDTO>>(products), Times.Once);
        }
    }
} 