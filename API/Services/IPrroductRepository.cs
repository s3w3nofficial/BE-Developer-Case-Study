using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        Product GetProduct(string slug);
        Task<List<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 0);
        int GetNumberOfProducts();
        Task<Product> CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
