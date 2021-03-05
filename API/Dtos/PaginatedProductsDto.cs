using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record PaginatedProductsDto
    {
        public IEnumerable<ProductDto> Products { get; init; }
        public int PageSize { get; init; }
        public int CurrentPage { get; init; }
        public int Total { get; init; }
    }
}
