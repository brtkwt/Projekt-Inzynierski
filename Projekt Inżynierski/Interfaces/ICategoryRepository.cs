using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<bool> CategoryNameExistsAsync(string newCategoryName);
        Task<Category> CreateCategoryAsync(string newCategoryName);
        Task<Category> UpdateCategoryAsync(int id, string newCategoryName);
        Task<int> CategoryHasProductsAsync(int id);
        Task<Category> DeleteCategoryAsync(int id);
    }
}
