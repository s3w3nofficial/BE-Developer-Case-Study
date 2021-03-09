using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Services;
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
            var existingProduct = this._categoryRepository.GetCategory(id);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "category-1",
                    Message = $"category with id: {id} not found"
                });

            return Ok(existingProduct.AsDto());
        }
    }
}
