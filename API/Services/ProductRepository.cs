using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductRepository : IProductRepository
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
                .OrderBy(p => p.Id)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

        public int GetNumberOfProducts() => this._db.Products.Count();

        public async Task<Product> CreateProductAsync(Product product)
        {
            await this._db.Products.AddAsync(product);
            await this._db.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            this._db.Products.Remove(product);
            await this._db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            this._db.Products.Update(product);
            await this._db.SaveChangesAsync();
        }
    }
}
