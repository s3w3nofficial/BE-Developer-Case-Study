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

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await this._db.Categories.AddAsync(category);
            await this._db.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            this._db.Categories.Remove(category);
            await this._db.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            this._db.Categories.Update(category);
            await this._db.SaveChangesAsync();
        }
    }
}
