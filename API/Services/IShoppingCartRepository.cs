using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IShoppingCartRepository
    {
        IEnumerable<Product> GetProductsInCart(string username);
        Task AddProductToCartAsync(string username, Product product);
        Task RemoveProductFromCartAsync(string username, Product product);
    }
}
