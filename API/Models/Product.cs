using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public record Product
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImgUri { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public Category Category { get; set; }
    }
}
