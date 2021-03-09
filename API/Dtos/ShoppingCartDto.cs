using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record ShoppingCartDto
    {
        public IEnumerable<ProductDto> Products { get; init; }
        public decimal TotalPrice { get => this.Products.Sum(p => p.Price); }
    }
}
