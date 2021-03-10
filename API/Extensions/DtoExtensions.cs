using API.Dtos;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DtoExtensions
    {
        public static ProductDto AsDto(this Product product)
            => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ImgUri = product.ImgUri,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category.AsDto(),
            };

        public static CategoryDto AsDto(this Category category)
            => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
    }
}
