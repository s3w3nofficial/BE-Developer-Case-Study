using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
