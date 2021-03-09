using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICategoryRepository
    {
        Category GetCategory(Guid id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
