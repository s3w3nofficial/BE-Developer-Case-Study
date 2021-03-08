using API.Dtos;
using API.Models;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public static class SlugService
    {
        public static string Slugify(Product product) => $"{(new SlugHelper()).GenerateSlug(product.Name)}-{product.Id.ToString().Split('-')[0]}";
        public static string Slugify(ProductDto productDto) => $"{(new SlugHelper()).GenerateSlug(productDto.Name)}-{productDto.Id.ToString().Split('-')[0]}";
    }
}
