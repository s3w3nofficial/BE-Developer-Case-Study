﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public record Product
    {
        [Key]
        [Required]
        public Guid Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string ImgUri { get; init; }
        [Required]
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
    }
}
