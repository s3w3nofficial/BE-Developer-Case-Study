using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public Category GetCategory(Guid id)
            => this._db.Categories.FirstOrDefault(c => c.Id == id);

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
            => await this._db.Categories
                .OrderBy(c => c.Id)
                .ToListAsync();
    }
}
