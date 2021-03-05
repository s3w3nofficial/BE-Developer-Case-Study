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
        Product GetProduct(int id);
        Task<List<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 0);
        int GetNumberOfProducts();
        Task UpdateProductAsync(Product product);
    }
}
