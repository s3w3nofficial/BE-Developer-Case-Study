using API.Controllers;
using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class CategoriesControllerTests
    {
        public static IQueryable<Category> data = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "test-category-1"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "test-category-2"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "test-category-3"
            }
        }.AsQueryable();

        [Fact]
        public void GetCategory_WithUnExistingCategory_ReturnsNotFound()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetCategory_WithExistingCategory_ReturnsExpectedCategory()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = controller.Get(data.FirstOrDefault().Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as CategoryDto;
            Assert.Equal(data.First().Id, dto.Id);
            Assert.Equal(data.First().Name, dto.Name);
        }

        [Fact]
        public async Task GetCategoriesAsync_WithExistingCategories_ReturnsAllCategories()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var dto = okResult.Value as IEnumerable<CategoryDto>;
            Assert.Equal(dto.Count(), data.Count());
        }

        [Fact]
        public async Task UpdateCategoryAsync_WithUnExistingCategory_ReturnsNotFound()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = await controller.UpdateAsync(Guid.NewGuid(), new CategoryDto 
            { 
                Name = "abcdef"
            });

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCategoryAsync_WithExistingCategory_ReturnsNoContent()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = await controller.UpdateAsync(data.FirstOrDefault().Id, new CategoryDto 
            { 
                Name = "updated-category-name"
            });

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task DeleteCategoryAsync_WithUnExistingCategory_ReturnsNotFound()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = await controller.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategoryAsync_WithExistingCategory_ReturnsNoContent()
        {
            // Arrange
            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var repository = new CategoryRepository(mockContext.Object);

            var controller = new CategoriesController(repository);

            // Act
            var result = await controller.DeleteAsync(data.FirstOrDefault().Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
