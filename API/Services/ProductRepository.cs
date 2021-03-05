using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductRepository : IPrroductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public Product GetProduct(int id)
            => this._db.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

        public async Task<List<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 0)
            => await this._db.Products
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

        public int GetNumberOfProducts() => this._db.Products.Count();

        public async Task UpdateProductAsync(Product product)
        {
            this._db.Products.Update(product);
            await this._db.SaveChangesAsync();
        }
    }
}
