using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<Product> GetProductsInCart(string username)
            => this._db.Users
                .Include(u => u.Products)
                .OrderBy(p => p.Id)
                .FirstOrDefault(u => u.UserName == username).Products
                .ToList();

        public async Task AddProductToCartAsync(string username, Product product)
        {
            var user = this._db.Users
                .Include(u => u.Products)
                .OrderBy(p => p.Id)
                .FirstOrDefault(u => u.UserName == username);
            user.Products.Add(product);

            this._db.Users.Update(user);
            await this._db.SaveChangesAsync();
        }

        public async Task RemoveProductFromCartAsync(string username, Product product)
        {
            var user = this._db.Users
                .Include(u => u.Products)
                .OrderBy(p => p.Id)
                .FirstOrDefault(u => u.UserName == username);
            user.Products.Remove(product);

            this._db.Users.Update(user);
            await this._db.SaveChangesAsync();
        }
    }
}
