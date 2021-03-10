using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieves all CategoryDtos
        /// </summary>
        /// <returns>list of CategoryDtos</returns>
        /// <response code="200">Returns list of CategoryDtos</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var categories = await this._categoryRepository.GetCategoriesAsync();

            return Ok(categories.Select(c => c.AsDto()));
        }

        /// <summary>
        /// Retrieves specific CategoryDto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CategoryDto with specified id</returns>
        /// <response code="200">Returns specific CategoryDto</response>
        /// <response code="404">If category does not exist</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(Guid id)
        {
            var existingCategory = this._categoryRepository.GetCategory(id);

            if (existingCategory is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "category-1",
                    Message = $"category with id: {id} not found"
                });

            return Ok(existingCategory.AsDto());
        }

        /// <summary>
        /// Updates specific Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDto"></param>
        /// <response code="204">Returns when successfully updated</response>
        /// <response code="403">If user is not loged in or is not in admin role</response>
        /// <response code="404">If category does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            Guid id,
            [FromBody] CategoryDto categoryDto)
        {
            var existingCategory = this._categoryRepository.GetCategory(id);

            if (existingCategory is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "category-1",
                    Message = $"category with id: {id} not found"
                });

            Category updatedCategory = existingCategory with
            {
                Name = categoryDto.Name,
            };

            await this._categoryRepository.UpdateCategoryAsync(updatedCategory);

            return NoContent();
        }

        /// <summary>
        /// Creates new Category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns>Created Category as ProductDto</returns>
        /// <response code="201">Returns when successfully created</response>
        /// <response code="403">If user is not loged in or is not in admin role</response>
        /// <response code="400">If not all parameters are provided</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateAsync([FromBody] CategoryDto categoryDto)
        {
            Category newCategory = new()
            {
                Name = categoryDto.Name,
            };

            var category = await this._categoryRepository.CreateCategoryAsync(newCategory);

            return Created("", category.AsDto());
        }

        /// <summary>
        /// Deletes Category
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns when successfully deleted</response>
        /// <response code="403">If user is not loged in or is not in admin role</response>
        /// <response code="404">If category does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingCategory = this._categoryRepository.GetCategory(id);

            if (existingCategory is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "category-1",
                    Message = $"category with id: {id} not found"
                });

            await this._categoryRepository.DeleteCategoryAsync(existingCategory);

            return NoContent();
        }
    }
}
