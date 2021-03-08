using API.Controllers;
using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ProductsControllerTests
    {
        public static IQueryable<Product> data = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                ImgUri = "https://via.placeholder.com/600x400",
                Price = 39,
                Description = "Test Test Test"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test 2",
                ImgUri = "https://via.placeholder.com/600x400",
                Price = 339,
                Description = "Test 2 Test 2 Test"
            }
        }.AsQueryable();

        [Fact]
        public void GetProduct_WithUnExistingProduct_ReturnsNotFound()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            var result = controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetProduct_WithExistingProduct_ReturnsExpectedProduct()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            var result = controller.Get(data.FirstOrDefault().Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as ProductDto;
            Assert.Equal(data.First().Id, dto.Id);
            Assert.Equal(data.First().Name, dto.Name);
            Assert.Equal(data.First().ImgUri, dto.ImgUri);
            Assert.Equal(data.First().Price, dto.Price);
            Assert.Equal(data.First().Description, dto.Description);
        }

        [Fact]
        public async Task GetProductsAsync_WithExistingProducts_ReturnsAllProducts()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as IEnumerable<ProductDto>;
            Assert.Equal(dto.Count(), data.Count());
        }

        [Fact]
        public async Task GetProductsAsync_WithExistingProductsAndPageSizePlusPageNumber_ReturnsPaginatedProducts()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            int pageSize = 1;
            int pageNumber = 2;
            var result = await controller.GetAllWithPaginationAsync(pageSize, pageNumber);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as PaginatedProductsDto;
            Assert.Equal(dto.Products.Count(), pageSize);
            Assert.Equal(dto.PageSize, pageSize);
            Assert.Equal(dto.CurrentPage, pageNumber);
            Assert.Equal(dto.Total, data.Count());
        }

        [Fact]
        public async Task UpdateProductAsync_WithUnExistingProduct_ReturnsNotFound()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            var result = await controller.UpdateDescriptionAsync(Guid.NewGuid(), "abcd");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProductAsync_WithExistingProduct_ReturnsNoContent()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockContext.Object);

            var controller = new ProductsController(repository);

            // Act
            var result = await controller.UpdateDescriptionAsync(data.FirstOrDefault().Id, "abcd");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
