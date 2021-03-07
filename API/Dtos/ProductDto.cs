using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record ProductDto
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string ImgUri { get; init; }
        [Required]
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
    }
}
