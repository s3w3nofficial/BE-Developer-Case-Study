using API.Dtos;
using API.Extensions;
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
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        public ActionResult<ShoppingCartDto> GetAll()
        {
            var username = HttpContext.User.Identity.Name;
            var products = this._shoppingCartRepository.GetProductsInCart(username);

            return Ok(new ShoppingCartDto
            {
                Products = products.Select(p => p.AsDto()),
                TotalPrice = products.Sum(p => p.Price),
            });
        }
    }
}
