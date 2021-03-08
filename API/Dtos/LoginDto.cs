using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public record LoginDto
    {
        [Required]
        public string UserName { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
