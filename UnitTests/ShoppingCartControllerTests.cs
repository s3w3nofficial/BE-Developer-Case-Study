using API.Controllers;
using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ShoppingCartControllerTests
    {
        public static IQueryable<ApplicationUser> data = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "test@test.cz",
                Email = "test@test.cz",
                Products = new List<Product>
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
                }
            }
        }.AsQueryable();

        public static IQueryable<Product> dataProducts = new List<Product>
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
        public void GetProducts_WithExistingProducts_ReturnsShoppingCartDto()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();
            var productMockSet = dataProducts.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products).Returns(productMockSet.Object);

            var repository = new ShoppingCartRepository(mockContext.Object);
            var productRepository = new ProductRepository(mockContext.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.NameIdentifier, "TestUser"),
                new Claim(ClaimTypes.Name, "test@test.cz")
            }, "Bearer"));

            var controller = new ShoppingCartController(repository, productRepository);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as ShoppingCartDto;
            Assert.Equal(dto.Products.Count(), data.FirstOrDefault().Products.Count);
        }

        [Fact]
        public async void AddProductAsync_WithExistingProducts_ReturnsNoContent()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();
            var productMockSet = dataProducts.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products).Returns(productMockSet.Object);

            var repository = new ShoppingCartRepository(mockContext.Object);
            var productRepository = new ProductRepository(mockContext.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "TestUser"),
                new Claim(ClaimTypes.Name, "test@test.cz")
            }, "Bearer"));

            var controller = new ShoppingCartController(repository, productRepository);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = await controller.AddProductToShoppingCart(dataProducts.FirstOrDefault().Id.ToString());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteProductAsync_WithExistingProducts_ReturnsNoContent()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();
            var productMockSet = dataProducts.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products).Returns(productMockSet.Object);

            var repository = new ShoppingCartRepository(mockContext.Object);
            var productRepository = new ProductRepository(mockContext.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "TestUser"),
                new Claim(ClaimTypes.Name, "test@test.cz")
            }, "Bearer"));

            var controller = new ShoppingCartController(repository, productRepository);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = await controller.RemoveProductFromShoppingCart(dataProducts.FirstOrDefault().Id.ToString());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
