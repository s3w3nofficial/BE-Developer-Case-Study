using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public record Category
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
