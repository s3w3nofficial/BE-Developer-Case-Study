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

        public Product GetProduct(Guid id)
            => this._db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .FirstOrDefault(p => p.Id == id);

        public Product GetProduct(string slug)
            => this._db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToList()
                .OrderBy(p => p.Id)
                .FirstOrDefault(p => SlugService.Slugify(p) == slug);

        public async Task<List<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 0)
            => await this._db.Products
                .Include(p => p.Category)
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
