using API.Models;
using API.Services;
using Slugify;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string ImgUri { get; init; }
        [Required]
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string Slug { get => SlugService.Slugify(this); }
        public CategoryDto Category { get; set; }
    }
}
