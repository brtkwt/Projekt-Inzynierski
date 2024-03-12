using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Helpers;

namespace Projekt_Inżynierski.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductQueryObject query);
        Task<bool> ProductNameExistsAsync(string newProductName, int id = 0);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<Product> DeleteProductAsync(int id);
    }
}
