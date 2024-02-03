using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dtos;

namespace Projekt_Inżynierski.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<bool> ProductNameExistsAsync(string newProductName);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, CreateProductRequestDto productDto, Category category, Company company);
        Task<Product> DeleteProductAsync(int id);
    }
}
