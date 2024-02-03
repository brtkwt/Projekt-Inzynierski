using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> CategoryNameExistsAsync(string newCategoryName)
        {
            return await _context.Categories.AnyAsync(c => c.Name == newCategoryName);
        }

        public async Task<Category> CreateCategoryAsync(string newCategoryName)
        {
            Category newCategory = new Category()
            {
                Name = newCategoryName
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return newCategory;
        }

        public async Task<Category> UpdateCategoryAsync(int id, string newCategoryName)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if(existingCategory == null)
            {
                return null;    // ?????
            }

            existingCategory.Name = newCategoryName;
            await _context.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<int> CategoryHasProductsAsync(int id)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if(existingCategory == null)
            {
                return -1;    // ?????
            }

            return await _context.Products.CountAsync( x => x.CategoryId == id);
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }

    }
}
