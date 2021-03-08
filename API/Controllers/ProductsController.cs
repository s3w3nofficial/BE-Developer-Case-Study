using API.Dtos;
using API.Errors;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productService;
        public ProductsController(IProductRepository productService)
        {
            this._productService = productService;
        }

        /// <summary>
        /// Retrieves all ProductDtos
        /// </summary>
        /// <response code="200">Returns list of ProductDtos</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await this._productService
                .GetProductsAsync(this._productService.GetNumberOfProducts(), 0);

            return Ok(products.Select(p => p.AsDto()));
        }

        /// <summary>
        /// Retrieves PaginatedProductsDto that contains ProductDtos on specific page
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <response code="200">Returns PaginatedProductDto</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(GroupName = "v2")]
        [HttpGet("/api/v2/[controller]")]
        public async Task<ActionResult<PaginatedProductsDto>> GetAllWithPaginationAsync(
            [FromQuery] int pageSize = 10, int pageNumber = 1)
        {
            if (pageSize < 1)
                pageSize = 1;

            if (pageNumber < 1)
                pageNumber = 1;

            var products = await this._productService
                .GetProductsAsync(pageSize, pageNumber - 1);

            return Ok(new PaginatedProductsDto
            {
                Products = products.Select(p => p.AsDto()),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Total = this._productService.GetNumberOfProducts()
            });
        }

        /// <summary>
        /// Retrieves specific ProductDto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProductDto with specified id</returns>
        /// <response code="200">Returns specific ProductDto</response>
        /// <response code="404">If product does not exist</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> Get(int id)
        {
            var existingProduct = this._productService.GetProduct(id);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id: {id} not found"
                });
            return Ok(existingProduct.AsDto());
        }

        /// <summary>
        /// Updates description of specific ProductDto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <response code="204">Returns when successfully updated</response>
        /// <response code="404">If product does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpPut("{id:int}/description")]
        public async Task<IActionResult> UpdateDescriptionAsync(
            int id, 
            [FromBody] string description)
        {
            var existingProduct = this._productService.GetProduct(id);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id: {id} not found"
                });

            Product updatedProduct = existingProduct with
            {
                Description = description
            };

            await this._productService.UpdateProductAsync(updatedProduct);

            return NoContent();
        }

        /// <summary>
        /// Updates specific ProductDto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <response code="204">Returns when successfully updated</response>
        /// <response code="404">If product does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] ProductDto productDto)
        {
            var existingProduct = this._productService.GetProduct(id);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id: {id} not found"
                });

            Product updatedProduct = existingProduct with
            {
                Name = productDto.Name,
                ImgUri = productDto.ImgUri,
                Price = productDto.Price,
                Description = productDto.Description
            };

            await this._productService.UpdateProductAsync(updatedProduct);

            return NoContent();
        }

        /// <summary>
        /// Creates new Product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns>Created Product as ProductDto</returns>
        /// <response code="201">Returns when successfully created</response>
        /// <response code="400">If not all parameters are provided</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] ProductDto productDto)
        {
            Product newProduct = new()
            {
                Name = productDto.Name,
                ImgUri = productDto.ImgUri,
                Price = productDto.Price,
                Description = productDto.Description
            };

            var product = await this._productService.CreateProductAsync(newProduct);

            return Created("", product);
        }

        /// <summary>
        /// Deletes Product
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns when successfully deleted</response>
        /// <response code="404">If product does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingProduct = this._productService.GetProduct(id);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id: {id} not found"
                });

            await this._productService.DeleteProductAsync(existingProduct);

            return NoContent();
        }
    }
}
