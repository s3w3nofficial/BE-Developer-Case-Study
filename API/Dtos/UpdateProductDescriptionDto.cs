using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record UpdateProductDescriptionDto
    {
        [Required]
        public string Description { get; set; }
    }
}
