using API.Dtos;
using API.Extensions;
using API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Examples
{
    public class ProductDtoExample : IExamplesProvider<IEnumerable<ProductDto>>
    {
        public IEnumerable<ProductDto> GetExamples()
        {
            return new List<Product> 
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 39,
                    Description = "Test Test Test"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Test 2",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 339,
                    Description = "Test 2 Test 2 Test"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Test 3",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 499.50M,
                    Description = "Test 3 Test 3 Test"
                },
            }.Select(p => p.AsDto());
        }
    }
}
