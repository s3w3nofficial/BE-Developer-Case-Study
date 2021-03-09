using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        public ShoppingCartController(
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<ShoppingCartDto> GetAll()
        {
            var products = this._shoppingCartRepository
                .GetProductsInCart(HttpContext.User.Identity.Name);

            return Ok(new ShoppingCartDto
            {
                Products = products.Select(p => p.AsDto()),
            });
        }

        [HttpPost("{query}")]
        public async Task<IActionResult> AddProductToShoppingCart(string query)
        {
            bool isValid = Guid.TryParse(query, out Guid guidOutput);
            Product existingProduct;
            if (isValid)
                existingProduct = this._productRepository.GetProduct(guidOutput);
            else
                existingProduct = this._productRepository.GetProduct(query);
            

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id or slug: {query} not found"
                });

            await this._shoppingCartRepository
                .AddProductToCartAsync(HttpContext.User.Identity.Name, existingProduct);

            return NoContent();
        }

        [HttpDelete("{query}")]
        public async Task<IActionResult> RemoveProductFromShoppingCart(string query)
        {
            bool isValid = Guid.TryParse(query, out Guid guidOutput);
            Product existingProduct;
            if (isValid)
                existingProduct = this._productRepository.GetProduct(guidOutput);
            else
                existingProduct = this._productRepository.GetProduct(query);

            if (existingProduct is null)
                return NotFound(new ErrorDetails
                {
                    StatusCode = "product-1",
                    Message = $"product with id or slug: {query} not found"
                });

            await this._shoppingCartRepository.RemoveProductFromCartAsync(HttpContext.User.Identity.Name, existingProduct);

            return NoContent();
        }
    }
}
