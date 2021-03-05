using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImgUri { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
    }
}
